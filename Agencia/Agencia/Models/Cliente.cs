using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agencia.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Nome { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public string Endereco { get; set; }

        public int Cep { get; set; }

        public string Destino { get; set; }

        public int Quantidade { get; set; }

        public string Formapag { get; set; }

        internal int Count()
        {
            throw new NotImplementedException();
        }
    }
}
