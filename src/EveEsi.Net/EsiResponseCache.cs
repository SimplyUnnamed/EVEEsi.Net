using System.Net;

namespace EveEsi.Net;

public class EsiResponseCache
{
	public HttpStatusCode StatusCode { get; set; }

	public Dictionary<string, List<string>> ResponseHeaders { get; set; } = new();

	public byte[] ResponseContent { get; set; } = [];
}
