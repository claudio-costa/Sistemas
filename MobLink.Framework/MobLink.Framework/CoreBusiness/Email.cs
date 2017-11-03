using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MobLink.Framework.CoreBusiness
{
    public static class Email
    {
        public static CredencialEmail Institucional(Empresa.Setores Setor)
        {
            switch (Setor)
            {
                case Empresa.Setores.AutoAtendimento:
                    return new CredencialEmail()
                    {
                        Email = "system@mob-link.net.br",
                        Senha = "Studio55#",
                        Usuario = "BOLETO LINKPÁTIOS"
                    };
                    
                case Empresa.Setores.CEO:
                    return new CredencialEmail()
                    {
                        Email = "cristineysoares@mob-link.net.br",
                        Senha = "Studio55#",
                        Usuario = "Cristiney Soares - MobLink"
                    };

                case Empresa.Setores.Desenvolvimento:
                    return new CredencialEmail()
                    {
                        Email = "",
                        Senha = "",
                        Usuario = ""
                    };

                case Empresa.Setores.Diretoria:
                    return new CredencialEmail()
                    {
                        Email = "",
                        Senha = "",
                        Usuario = ""
                    };

                case Empresa.Setores.CEO_L2R:
                    return new CredencialEmail()
                    {
                        Email = "",
                        Senha = "",
                        Usuario = ""
                    };

                default:
                    return new CredencialEmail()
                    {
                        Email = "",
                        Senha = "",
                        Usuario = ""
                    };
            }
        }

        public static string Enviar(string Destinatario,
                                    string Assunto,
                                    string enviaMensagem,
                                    Stream anexo = null,
                                    string NomeAnexo = null)
        {
            try
            {
                // valida o email
                bool bValidaEmail = ValidaEnderecoEmail(Destinatario);

                if (bValidaEmail == false)
                    return "Email do destinatário inválido " + Destinatario;

                // Cria uma mensagem
                var from = new MailAddress(Institucional(Empresa.Setores.AutoAtendimento).Email,
                                           Institucional(Empresa.Setores.AutoAtendimento).Usuario);

                var to = new MailAddress(Destinatario);

                MailMessage mensagemEmail = new MailMessage(from, to);
                mensagemEmail.Subject = Assunto;
                mensagemEmail.Body = enviaMensagem;
                mensagemEmail.IsBodyHtml = true;

                if (anexo != null)
                {
                    Attachment anexado = new Attachment(anexo, NomeAnexo);
                    mensagemEmail.Attachments.Add(anexado);
                }

                SmtpClient client = new SmtpClient(Setup.Email.ServidorSMTP.Host,
                                                   Setup.Email.ServidorSMTP.Porta);

                client.EnableSsl = Setup.Email.ServidorSMTP.RequerSSL;

                if (Setup.Email.ServidorSMTP.RequerAutenticacao)
                {
                    NetworkCredential cred = new NetworkCredential(Institucional(Empresa.Setores.AutoAtendimento).Email,
                                                                   Institucional(Empresa.Setores.AutoAtendimento).Senha);

                    client.Credentials = cred;
                }
                //}
                //else
                //    client.UseDefaultCredentials = true;

                // envia a mensagem
                client.Send(mensagemEmail);

                return "Mensagem enviada para " + Destinatario + " às " + DateTime.Now.ToString() + ".";
            }
            catch (Exception ex)
            {
                string erro = ex.InnerException.ToString();

                return ex.Message.ToString() + erro;
            }
        }

        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                //define a expressão regulara para validar o email
                string texto_Validar = enderecoEmail;
                //Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                Regex expressaoRegex = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");



                // testa o email com a expressão
                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    // o email é valido
                    return true;
                }
                else
                {
                    // o email é inválido
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<MailMessage> Receber()
        {
            return new List<MailMessage>();
        }
    }
}
