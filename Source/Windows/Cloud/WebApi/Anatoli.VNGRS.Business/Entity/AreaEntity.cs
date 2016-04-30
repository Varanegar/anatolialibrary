using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingMap.Common.ViewModel;

namespace TrackingMap.Service.Entity
{
    [Table("Area")]
    public class AreaEntity : BaseEntityAutoIncId
    {
        [Column("ParentId")]
        public Guid? ParentId { set; get; }

        [Column("Code", TypeName = "int")]
        public int Code { set; get; }

        [Column("LeftCode", TypeName = "int")]
        public int LeftCode { set; get; }
        
        [Column("RightCode", TypeName = "int")]
        public int RightCode { set; get; }


        [Column("Title", TypeName = "varchar")]
        [MaxLength(200)]
        public string Title { set; get; }

        [Column("IsLeaf", TypeName = "bit")]
        public bool IsLeaf { set; get; }

        public AreaView GetView(){
            return new AreaView() { Id = this.Id, Title = this.Title, IsLeaf = this.IsLeaf};
        }
    }
}
