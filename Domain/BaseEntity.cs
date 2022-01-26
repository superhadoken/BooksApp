using System;

namespace Domain
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        // primary key (would normally use int identity column and have EntityFramework manage primary keys but for simplicity/brevity used Guids)
        // use int/identity column for faster indexing purposes instead of Guid or string (guids/string can lead to page split in clustered index 
        // where data is entered in the middle of the page and ultimately fragmentation)
        public Guid Id { get; private set; }
    }
}
