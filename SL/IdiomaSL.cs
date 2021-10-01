using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BE;
using Newtonsoft.Json;

namespace SL
{
    public class IdiomaSL
    {
        private EncriptacionSL gestorEncriptacion = new EncriptacionSL();

        public int Insertar(IdiomaBE idioma)
        {
            IdiomaMapper m = new IdiomaMapper();
            return m.Insertar(idioma);
        }

        public List<IdiomaBE> ListarIdiomas(IdiomaBE idioma)
        {
            IdiomaMapper m = new IdiomaMapper();
            return m.Listar(idioma);
        }

        public IdiomaBE ListarIdioma(IdiomaBE idioma)
        {
            IdiomaMapper m = new IdiomaMapper();
            IdiomaBE idiomaRet = new IdiomaBE();
            foreach (IdiomaBE idio in m.Listar(idioma))
            {
                if (idioma.IdIdioma == idio.IdIdioma)
                {
                    idio.Textos = m.ListarTextosDelIdioma(idio);
                    idiomaRet = idio;
                    break;
                }
            }
            return idiomaRet;
        }

        //public void SetearIdioma(ref UsuarioBE usuario)
        //{
        //    IdiomaMapper m = new IdiomaMapper();
        //    m.SetearIdioma(ref usuario);
        //}

        public List<TextoBE> ListarTextosDelIdioma(IdiomaBE idioma)
        {
            IdiomaMapper m = new IdiomaMapper();
            return m.ListarTextosDelIdioma(idioma);
        }

        public string TraducirTexto(IdiomaBE idioma, int codTexto)
        {
            string TextoTraducido = string.Empty;

            if ((idioma !=null) && (idioma.Textos != null))
            foreach (TextoBE texto in idioma.Textos)
            {
                if (texto.IdFrase == codTexto)
                {
                    TextoTraducido = texto.Texto;
                }
            }
            return TextoTraducido;
        }

        public int TraducirNombresDeIdiomas()
        {
            //string NombreTagRemitenteEncriptado = gestorEncriptacion.SimpleEncrypt("MailSender");
            //string RemitenteEncriptado = ConfigurationManager.AppSettings[NombreTagRemitenteEncriptado];
            //string remitente = gestorEncriptacion.SimpleDecrypt(RemitenteEncriptado);
            int retVal = 0;
            try
            {
                IdiomaMapper m = new IdiomaMapper();
                List<Tuple<IdiomaBE, IdiomaBE, string>> listaTraducir = m.ListarNombresDeIdiomasParaTraducir();
                HttpClient client = new HttpClient();
                foreach (Tuple<IdiomaBE, IdiomaBE, string> item in listaTraducir)
                {
                    string requestStr = String.Format("?q={0}&langpair={1}|{2}&de={3}", item.Item3, "es",
                            ((IdiomaBE)item.Item2).CodIdioma, "aripaudev@gmail.com");

                    TranslationResponse.Rootobject tResponse = new TranslationResponse.Rootobject();
                    string jsonResp = client.GetStringAsync("https://api.mymemory.translated.net/get" + requestStr).Result;
                    tResponse = JsonConvert.DeserializeObject<TranslationResponse.Rootobject>(jsonResp);

                    if (tResponse != null)
                    {
                        ////if matches.count??
                        retVal += m.ActualizarNombreIdioma((IdiomaBE)item.Item1, (IdiomaBE)item.Item2, tResponse.responseData.translatedText);
                    }
                }
            }
            catch (Exception)
            {
                //-1: Idioma no existe.
                //Revisar si hay un tipo de excepcion específica
                //-2: No hay conexión a internet o el servicio falla.
                //Revisar la excepcion en este caso
                //-9 error desconocido
                retVal = -9;
                IdiomaMapper m = new IdiomaMapper();
                //m.EliminarTextosDeIdioma(idiomaDestino);
                //m.EliminarIdioma(idiomaDestino);
            }
            return retVal;
        }

        public int TraducirIdiomaCompleto(IdiomaBE idiomaOrigen, IdiomaBE idiomaDestino)
        {
            int retVal = 0;
            try
            {
                IdiomaMapper m = new IdiomaMapper();
                List<TextoBE> textosOrigen = ListarTextosDelIdioma(idiomaOrigen);
                HttpClient client = new HttpClient();
                //Por las dudas borro antes lo del destino, por si reutilizo esta función en otro momento
                m.EliminarTextosDeIdioma(idiomaDestino);
                //**************************************
                //TODO: Agregar esta parte correctamente
                //**************************************
                string NombreTagRemitenteEncriptado = gestorEncriptacion.SimpleEncrypt("MailSender");
                string RemitenteEncriptado = ConfigurationManager.AppSettings[NombreTagRemitenteEncriptado];
                string remitente = gestorEncriptacion.SimpleDecrypt(RemitenteEncriptado);

                foreach (TextoBE text in textosOrigen)
                {
                    string requestStr = String.Format("?q={0}&langpair={1}|{2}&de={3}", text.Texto, idiomaOrigen.CodIdioma,
                        idiomaDestino.CodIdioma, remitente);

                    TranslationResponse.Rootobject tResponse = new TranslationResponse.Rootobject();
                    string jsonResp = client.GetStringAsync("https://api.mymemory.translated.net/get" + requestStr).Result;
                    tResponse = JsonConvert.DeserializeObject<TranslationResponse.Rootobject>(jsonResp);

                    if (tResponse != null)
                    {
                        //if matches.count??
                        TextoBE textoTraducido = new TextoBE();
                        textoTraducido.IdFrase = text.IdFrase;
                        textoTraducido.Texto = tResponse.responseData.translatedText;
                        retVal += m.InsertarTexto(idiomaDestino, textoTraducido);
                    }
                }
            }
            catch (Exception)
            {
                //-1: Idioma no existe.
                //Revisar si hay un tipo de excepcion específica
                //-2: No hay conexión a internet o el servicio falla.
                //Revisar la excepcion en este caso
                //-9 error desconocido
                retVal = -9;
                IdiomaMapper m = new IdiomaMapper();
                m.EliminarTextosDeIdioma(idiomaDestino);
                m.EliminarIdioma(idiomaDestino);
            }
            TraducirNombresDeIdiomas();
            return retVal;
        }

        public int ActualizarTexto(IdiomaBE idioma, TextoBE texto)
        {
            IdiomaMapper m = new IdiomaMapper();
            return m.ActualizarTexto(idioma, texto);
        }
    }
}
