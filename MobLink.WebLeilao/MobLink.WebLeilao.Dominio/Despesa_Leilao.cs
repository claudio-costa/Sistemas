namespace MobLink.WebLeilao.Dominio
{
    public class Despesa_Leilao
    {
        public int Id { get; set; }
        public int Id_Leilao { get; set; }
        public int Id_Despesa { get; set; }
        public double Valor { get; set; }

        public virtual string DescricaoLeilao { get; set; }
        public virtual string DescricaoDespesa { get; set; }

        public virtual double Rateio { get; set; }
    }
}
