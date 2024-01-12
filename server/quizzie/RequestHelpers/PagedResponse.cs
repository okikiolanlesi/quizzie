using System;

namespace Quizzie.RequestHelpers;

public class PagedResponse<T>
{

    public T results { get; set; }
    public int totalCount { get; set; }
    public int page { get; set; }
    public int pageSize { get; set; }

}
