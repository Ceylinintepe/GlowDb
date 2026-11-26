namespace GlowDb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urunler()
        {
            this.AlisverisListesi = new HashSet<AlisverisListesi>();
            this.Favoriler = new HashSet<Favoriler>();
        }
    
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public string Marka { get; set; }
        public string Aciklama { get; set; }
        public Nullable<int> KategoriID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlisverisListesi> AlisverisListesi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favoriler> Favoriler { get; set; }
        public virtual Kategoriler Kategoriler { get; set; }
    }
}
