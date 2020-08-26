using TestDrive.Core;

namespace TestDrive.Models
{
    public class Veiculo : EntityBase
    {
        public const decimal FREIO_ABS = 800;
        public const decimal AR_CONDICIONADO = 1000;
        public const decimal MP3_PLAYER = 500;

        public string Nome { get; set; } = "";
        public decimal Preco { get; set; } = 0;

        public bool TemFreioABS { get; set; }
        public bool TemArCondicionado { get; set; }
        public bool TemMP3Player { get; set; }

        public decimal PrecoTotal => Preco
                    + (TemFreioABS ? FREIO_ABS : 0)
                    + (TemArCondicionado ? AR_CONDICIONADO : 0)
                    + (TemMP3Player ? MP3_PLAYER : 0);

    }
}
