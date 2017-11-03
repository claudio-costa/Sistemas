using MobLink.Framework;
using MobLink.Framework.Database;

namespace MobLink.LinkLeiloes.Repositorio
{
    public class RepositorioGlobal : DbSqlServer
    {
        public RepositorioGlobal(string ConnectionString) : base(ConnectionString)
        {
        }

        public enum Status
        {
            Ativo = 'A', Inativo = 'I', Finalizado='F', Pagamento='P'
        }

        public static class Util
        {
            public static Dominio.Endereco ConsultaCEP(string CEP)
            {
                Dominio.Endereco Endereco;

                var res = Framework.WebServices.WebserviceCorreios.ConsultaCEP(CEP);

                if (res != null)
                {
                    Endereco = new Dominio.Endereco()
                    {
                        bairro = res.bairro,
                        cep = res.cep,
                        cidade = res.cidade,
                        complemento = res.complemento,
                        complemento2 = res.complemento2,
                        end = res.end,
                        Id = res.Id,
                        uf = res.uf
                    };
                }
                else return new Dominio.Endereco();


                return Endereco;
            }

            public static System.Data.DataTable ConsultaGenerica(string connection_string, string sql)
            {
                var repositorio_generico = new RepositorioGlobal(connection_string);

                return repositorio_generico.ConsultaSQL(sql);
            }

            public static int ExecutaSqlGenerico(string connection_string, string sql)
            {
                var repositorio_generico = new RepositorioGlobal(connection_string);

                return repositorio_generico.ExecutaSQL(sql);
            }
        }

        public static ArrematanteRepositorio Arrematante
        {
            get { return new ArrematanteRepositorio(); }
        }

        public static ClienteRepositorio Cliente
        {
            get { return new ClienteRepositorio(); }
        }

        public static LoteRepositorio Lote
        {
            get { return new LoteRepositorio(); }
        }

        public static LeilaoRepositorio Leilao
        {
            get { return new LeilaoRepositorio(); }
        }

        public static ProprietarioRepositorio Proprietario
        {
            get { return new ProprietarioRepositorio(); }
        }

        public static StatusLoteRepositorio StatusLote
        {
            get { return new StatusLoteRepositorio(); }
        }

        public static PericiaRepositorio Pericia
        {
            get { return new PericiaRepositorio(); }
        }

        public static RestricaoRepositorio Restricao
        {
            get { return new RestricaoRepositorio(); }
        }

        public static ComitenteRepositorio Comitente
        {
            get { return new ComitenteRepositorio(); }
        }

        public static FinanceiraRepositorio Financeira
        {
            get { return new FinanceiraRepositorio(); }
        }

        public static StatusLeilaoRepositorio StatusLeilao
        {
            get { return new StatusLeilaoRepositorio(); }
        }

        public static ExpositorRepositorio Expositor
        {
            get { return new ExpositorRepositorio(); }
        }

        public static LeiloeiroRepositorio Leiloeiro
        {
            get { return new LeiloeiroRepositorio(); }
        }

        public static UsuarioRepositorio Usuario
        {
            get { return new UsuarioRepositorio(); }
        }

        public static TransacoesRepositorio Transacoes
        {
            get { return new TransacoesRepositorio(); }
        }

        public static DespesaRepositorio Despesa
        {
            get { return new DespesaRepositorio(); }
        }

        public static NotificacaoRepositorio Notificacao
        {
            get { return new NotificacaoRepositorio(); }
        }

        public static EmpresaRepositorio Empresa
        {
            get { return new EmpresaRepositorio(); }
        }

        public static DepositoRepositorio Deposito
        {
            get { return new DepositoRepositorio(); }
        }

        public static EditalRepositorio Edital
        {
            get { return new EditalRepositorio(); }
        }

        public static GRVRepositorio GRV
        {
            get { return new GRVRepositorio(); }
        }

        public static FotoRecolhimentoRepositorio FotoRecolhimento
        {
            get { return new FotoRecolhimentoRepositorio(); }
        }

        public static TipoVeiculoRepositorio TipoVeiculo
        {
            get { return new TipoVeiculoRepositorio(); }
        }

        public static CorRepositorio Cor
        {
            get { return new CorRepositorio(); }
        }

        public static NotificaoLeilaoRepositorio NotificaoLeilao
        {
            get { return new NotificaoLeilaoRepositorio(); }
        }

        public static BoletosRepositorio Boletos
        {
            get { return new BoletosRepositorio(); }
        }

        public static SapRepositorio Sap
        {
            get { return new SapRepositorio(); }
        }

        public static DPRepositorio DP
        {
            get { return new DPRepositorio(); }
        }

        public static RegraPrestacaoContaRepositorio RegraPrestacaoConta
        {
            get { return new RegraPrestacaoContaRepositorio(); }
        }
    }
}
