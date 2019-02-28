using System;
using DnDRoller.API.Domain.Common;

namespace DnDRoller.API.Domain.Entities
{
    public class EntityBase : IEnityBase
    {
        public Guid Id { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public void SetInitiallDefaults()
        {
            this.Id = Guid.NewGuid();
            this.CreatedBy = "SYSTEM";
            this.ModifiedBy = "SYSTEM";
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = DateTime.Now;
            this.IsDeleted = false;
        }
    }
}
