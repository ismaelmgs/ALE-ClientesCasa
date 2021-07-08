using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Data;
using System.Configuration;
using NucleoBase.Core;
using ClientesCasa.DomainModel;

namespace ClientesCasa.Clases
{
    public class ClsSecurity
    {
        /// <summary>
        /// Verifica si se trata de un acceso valido
        /// </summary>
        /// <param name="sUser">Usuario a validar</param>
        /// <param name="sPass">Contrase;a del usuario a validar</param>
        /// <returns></returns>
        public static UserIdentity IsValidAD(string sUser, string sPass)
        {
            UserIdentity oError = new UserIdentity();
            try
            {
                if (new DBUsuarios().DBEXisteUsuarioActivo(sUser))
                {
                    oError = GetNameAndStatusUser(SearchUserAD(sUser, sPass), sUser);

                    if (oError.sEstatus != string.Empty)
                    {
                        oError.bEncontrado = true;
                    }
                    else
                    {
                        DataTable dtU = new DBUsuarios().DBGetUsuario(sUser);

                        if (dtU.Rows.Count > 0)
                        {
                            oError.iRol = dtU.Rows[0]["IdRol"].S().I();
                            oError.sRolDescripcion = dtU.Rows[0]["RolDescripcion"].S();
                            oError.sUrlPaginaInicial = dtU.Rows[0]["UrlPaginaInicial"].S();
                            oError.sCorreoBaseUsuario = dtU.Rows[0]["CorreoBaseUsuario"].S();
                        }
                    }
                }
                else
                {
                    oError.bEncontrado = true;
                    oError.sEstatus = "El usuario %" + sUser + "% no se ha registrado en sistema, favor de verificar sus datos";
                }
            }
            catch
            {
                oError.bEncontrado = true;
                oError.sEstatus = "El usuario %" + sUser + "% no se encontró, favor de verificar sus datos";
            }

            return oError;
        }
        private static SearchResult SearchUserAD(string UserName, string Pass)
        {
            try
            {
                string _path = ConfigurationManager.AppSettings["PATH_LDAP"].ToString();
                string Dominio = ConfigurationManager.AppSettings["DOM_LDAP"].ToString();

                string domainAndUsername = Dominio + @"\" + UserName;
                DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, Pass);

                object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + UserName + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static UserIdentity GetNameAndStatusUser(SearchResult result, string sUser)
        {
            UserIdentity oErrorUser = new UserIdentity();
            try
            {
                if (result != null)
                {
                    DirectoryEntry deUser = result.GetDirectoryEntry();
                    int userAccountControlValue = (int)deUser.Properties["userAccountControl"].Value;
                    Enumeraciones.EnumUserAccountControl userAccountControl = (Enumeraciones.EnumUserAccountControl)userAccountControlValue;

                    oErrorUser.sUsuario = sUser;
                    oErrorUser.sName = deUser.Properties["givenName"].Value.S() + " " + deUser.Properties["sn"].Value.S();

                    switch (userAccountControl)
                    {
                        case Enumeraciones.EnumUserAccountControl.ACCOUNTDISABLE:
                            oErrorUser.bEncontrado = true;
                            oErrorUser.sEstatus = "El usuario %" + sUser.S() + "% se encuentra deshabilitado.";
                            break;
                        case Enumeraciones.EnumUserAccountControl.LOCKOUT:
                            oErrorUser.bEncontrado = true;
                            oErrorUser.sEstatus = "El usuario %" + sUser.S() + "% se encuentra bloqueado.";
                            break;
                        default:
                            oErrorUser.bEncontrado = false;
                            oErrorUser.sEstatus = string.Empty;
                            break;
                    }
                }
                else
                {
                    oErrorUser.bEncontrado = false;
                    oErrorUser.sEstatus = "El usuario %" + sUser.S() + "% no existe, favor de verificar.";
                }

                return oErrorUser;
            }
            catch
            {
                oErrorUser.bEncontrado = false;
                return oErrorUser;
            }
        }
    }
}