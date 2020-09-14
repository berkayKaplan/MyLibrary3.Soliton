using MyLibrary.Common.Helpers;
using MyLibrary.Entities;
using MyLibrary.Entities.Masseges;
using MyLibrary3.Entites.ValueObject;
using MyLibraryBusinessLayer.Abstract;
using MyLibraryBusinessLayer.Results;
using MyLibraryDataAccessLayer.E.F;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryBusinessLayer
{
    public class LibraryUserManger:ManagerBase<LibraryUser>
    {
        
        public BusinessLayerRusult <LibraryUser> RegisterUser(RegisterViewModel data)
        {
          LibraryUser user= Find(x => x.Username == data.Username || x.Email == data.EMail);
            BusinessLayerRusult<LibraryUser> res = new BusinessLayerRusult<LibraryUser>();
            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (user.Email == data.EMail)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                int dbResult = base.Insert(new LibraryUser()
                {
                    Username = data.Username,
                    Email = data.EMail,
                   
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false
                });

                if (dbResult > 0)
                {
                    res.Result = Find(x => x.Email == data.EMail && x.Username == data.Username);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.Username};<br><br>Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>.";

                    MailHelper.SendMail(body, res.Result.Email, "MyLibrary Hesap Aktifleştirme");
                }
            }
            return res;
        }

        public BusinessLayerRusult<LibraryUser> GetUserById(int id)
        {
            BusinessLayerRusult<LibraryUser> res = new BusinessLayerRusult<LibraryUser>();
            res.Result = Find(x => x.Id == id);
            if (res.Result==null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulanamadı.");
            }
            return res;
        }

      

        public BusinessLayerRusult<LibraryUser> UpdateProfile(LibraryUser data)
        {
            LibraryUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            BusinessLayerRusult<LibraryUser> res = new BusinessLayerRusult<LibraryUser>();

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;

           if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");
            }

            return res;
        }

        public BusinessLayerRusult<LibraryUser> RemoveUserById(int id)
        {
            BusinessLayerRusult<LibraryUser> res = new BusinessLayerRusult<LibraryUser>();
            LibraryUser user = Find(x => x.Id == id);

            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi.");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı bulunamadı.");
            }

            return res;
        }

        public  BusinessLayerRusult<LibraryUser> LoginUser(LoginViewModel data)
        {
        
            BusinessLayerRusult<LibraryUser> res = new BusinessLayerRusult<LibraryUser>();
            res.Result = Find(x => x.Username == data.Username && x.Password == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı yada şifre uyuşmuyor.");
            }

            return res;
        }
       
       
        public BusinessLayerRusult<LibraryUser> ActivateUser(Guid activateId)
        {
            BusinessLayerRusult<LibraryUser> res = new BusinessLayerRusult<LibraryUser>();
            res.Result = Find(x => x.ActivateGuid == activateId);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }

              
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
            }

            return res;
        }

        public new BusinessLayerRusult<LibraryUser> Insert(LibraryUser data)
        {
            LibraryUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerRusult<LibraryUser> res = new BusinessLayerRusult<LibraryUser>();
            res.Result = data;
            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                res.Result.ActivateGuid = Guid.NewGuid();

                if (base.Insert(res.Result)==0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı eklenemedi.");
                }
               
            }
            return res;
        }
        public new BusinessLayerRusult<LibraryUser> Update(LibraryUser data)
        {
            LibraryUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            BusinessLayerRusult<LibraryUser> res = new BusinessLayerRusult<LibraryUser>();
            res.Result = data;
            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            res.Result.IsActive = data.IsActive;
            res.Result.IsAdmin = data.IsAdmin;

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Kullanıcı güncellenemedi.");
            }

            return res;
        }

    }
}
