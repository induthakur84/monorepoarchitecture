namespace SharedModel
{

    //{
    //Pagenumber: 1
    //PAgesize: 10,
    //TotalNumberOfRecords: 1000,
    //Results: [
    //]
    //}
    public class PagedResults<T>
    {
        /// <summary>
        /// The current page this page represents. 
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary> 
        /// The size of this page. 
        /// </summary> 
        public int PageSize { get; set; }

        /// <summary> 
        /// The total number of records available. 
        /// </summary> 
        public int TotalNumberOfRecords { get; set; } //user can use this to calculate the total number of pages,
                                                      //or we can provide it as a property as well

        /// <summary> 
        /// The records this page represents. 
        /// </summary> 
        public IEnumerable<T> Results { get; set; }
    }
}
