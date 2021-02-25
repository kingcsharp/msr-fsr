using MSR.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Domain.Models.Paging
{
    public class PagingModel
    {
        public PagingModel()
        {
            PageNumber = 0;
            PageSize = 25;
            Term = string.Empty;
        }

        public string Term { get; set; }
        public int PageNumber { get; set; }
        public long TotalNumberOfRecords { get; set; }
        public int PageSize { get; set; }
        public string FilterProperty { get; set; }
        public bool IsDeleted { get; set; }

        public int Skip
        {
            get { return PageNumber * PageSize; }
        }

        public int Take
        {
            get { return PageSize; }
        }
        public bool SortAscending { get; set; }

        /// <summary>
        /// REMEMBER: that the method 'Skip' is only supported for SORTED input in LINQ to Entities.
        /// The method 'OrderBy' must be called before the method 'Skip'.
        /// </summary>
        public ICollection<T> Paginate<T>(IQueryable<T> sortedIQueryable)
        {
            TotalNumberOfRecords = sortedIQueryable.Count();
            return TotalNumberOfRecords > 0 ? sortedIQueryable.Skip(Skip).Take(Take).ToList() : new List<T>();
        }

        public ICollection<T> SortPaginate<T>(IQueryable<T> sortedIQueryable)
        {

            TotalNumberOfRecords = sortedIQueryable.Count();
            try
            {
                return TotalNumberOfRecords > 0 ?
                    sortedIQueryable.OrderByField(FilterProperty, SortAscending).Skip(Skip).Take(Take).ToList() : new List<T>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
