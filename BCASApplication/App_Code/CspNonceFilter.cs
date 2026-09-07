using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

// SA May-2026: response filter that injects a per-request CSP nonce into every <script> tag,
// enabling nonce-based CSP without modifying each ASPX page individually.
// Buffers the full response before processing to avoid chunk-boundary regex misses.
public class CspNonceFilter : Stream
{
    private readonly Stream _inner;
    private readonly string _nonce;
    private readonly MemoryStream _buffer;
    private bool _committed;

    public CspNonceFilter(Stream inner, string nonce)
    {
        _inner = inner;
        _nonce = nonce;
        _buffer = new MemoryStream();
    }

    public override bool CanRead  { get { return false; } }
    public override bool CanSeek  { get { return false; } }
    public override bool CanWrite { get { return true;  } }

    public override long Length
    {
        get { throw new NotSupportedException(); }
    }

    public override long Position
    {
        get { throw new NotSupportedException(); }
        set { throw new NotSupportedException(); }
    }

    public override void Flush()
    {
        Commit();
        _inner.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _buffer.Write(buffer, offset, count);
    }

    private void Commit()
    {
        if (_committed) return;
        _committed = true;
        var ctx = HttpContext.Current;
        byte[] data = _buffer.ToArray();
        if (ctx != null &&
            ctx.Response.ContentType != null &&
            ctx.Response.ContentType.StartsWith("text/html", StringComparison.OrdinalIgnoreCase))
        {
            Encoding enc = ctx.Response.ContentEncoding ?? Encoding.UTF8;
            string html = enc.GetString(data);
            string result = Regex.Replace(html,
                @"<script\b(?![^>]*\bnonce\s*=)",
                "<script nonce=\"" + _nonce + "\"",
                RegexOptions.IgnoreCase);
            byte[] output = enc.GetBytes(result);
            _inner.Write(output, 0, output.Length);
        }
        else
        {
            _inner.Write(data, 0, data.Length);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Commit();
            _buffer.Dispose();
        }
        base.Dispose(disposing);
    }
}
