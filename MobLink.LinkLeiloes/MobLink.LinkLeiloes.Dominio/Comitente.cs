namespace MobLink.WebLeilao.Dominio
{
    public class Comitente
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string contrato { get; set; }
        public string documento { get; set; }
        public string status { get; set; }
        public int? id_usuario_cadastro { get; set; }
        public string usuario_cadastro { get; set; }
        public int? id_cliente { get; set; }
        public int? iss { get; set; }
        public string cor { get; set; }
        public int tipo_importacao { get; set; }
    }
}