namespace MobLink.LinkLeiloes.Dominio
{
    public class Transacao
    {
        public int id { get; set; }
        public int id_lote { get; set; }
        public string transacao { get; set; }
        public string retorno { get; set; }
        public string data_hora { get; set; }
    }
}
