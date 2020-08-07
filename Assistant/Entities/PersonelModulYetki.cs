using System.ComponentModel.DataAnnotations.Schema;

namespace Assistant.Entities
{
    [Table("PersonelModulYetki")]
    public partial class PersonelModulYetki
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? PersonelId { get; set; }

        public int? ModulId { get; set; }

        public int? YetkiId { get; set; }

        public virtual Modul Modul { get; set; }

        public virtual Personel Personel { get; set; }

        public virtual Yetki Yetki { get; set; }
    }
}