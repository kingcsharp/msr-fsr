using MSR.Domain.Commanding.Abstractions;

namespace MSR.Domain.Commanding
{
    public abstract class PagingCommand: Command
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? Skip
        {
            get { 
                    if(PageNumber == null || PageSize == null) { 
                        return null;
                    }

                    return (int)(PageNumber * PageSize); 
                }
        }
        public int? Take
        {
            get { 
                    if(PageSize == null) { 
                        return null;
                    }

                    return (int)PageSize; 
                }
        }
        public string? Term { get; set; }
        public bool? SortAscending { get; set; }
    }

    public abstract class PagingCommand<TResult> : Command, ICommand<TResult>, ICommand
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? Skip
        {
            get
            {
                if (PageNumber == null || PageSize == null)
                {
                    return null;
                }

                return (int)(PageNumber * PageSize);
            }
        }
        public int? Take
        {
            get
            {
                if (PageSize == null)
                {
                    return null;
                }

                return (int)PageSize;
            }
        }
        public string? Term { get; set; }
        public bool? SortAscending { get; set; }
    }
}
