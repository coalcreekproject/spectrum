using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Repositories;

namespace Spectrum.Web.Controllers.Web
{

    public class UserProfileController : Controller
    {
        private ICoreUnitOfWork _coreUnitOfWork;
        private CoreDbContext _context;
        private UserRepository _userRepository;
        private readonly UserManager<User, int> _manager;
        private readonly UserProfileRepository _profileRepository;

        public UserProfileController(ICoreUnitOfWork uow)
        {
            _coreUnitOfWork = uow;
            _context = uow.Context;
            _userRepository = new UserRepository(uow);
            _manager = new UserManager<User, int>(_userRepository);
            _profileRepository = new UserProfileRepository(uow);
        }


        public ActionResult UserProfileIndex(int id)
        {
            //if (id == default(int))
            //{
            //    return HttpNotFound();
            //}

            //User user = _manager.FindById(id);

            //if (user == null)
            //{
            //    return HttpNotFound();
            //}

            //ViewBag.UserId = id;

            //var profiles = _profileRepository.All.Where(u => u.UserId == id);

            //var profileModels = new List<UserProfileViewModel>();

            //foreach (UserProfile profile in profiles)
            //{
            //    UserProfileViewModel userProfile = new UserProfileViewModel()
            //    {
            //        UserId = profile.UserId,
            //        OrganizationId = profile.OrganizationId,
            //        Default = profile.Default,
            //        ProfileName = profile.ProfileName,
            //        DstAdjust = profile.DstAdjust,
            //        FirstName = profile.FirstName,
            //        Id = profile.Id,
            //        Language = profile.Language,
            //        LastName = profile.LastName,
            //        MiddleName = profile.MiddleName,
            //        Nickname = profile.Nickname,
            //        SecondaryEmail = profile.SecondaryEmail,
            //        SecondaryPhoneNumber = profile.SecondaryPhoneNumber,
            //        TimeZone = profile.TimeZone,
            //        Title = profile.Title
            //    };

            //    profileModels.Add(userProfile);
            //}

            //return View(profileModels);
            return View();
        }

        //public ActionResult Details(int id)
        //{
        //    UserProfile profile = _context.UserProfiles.FirstOrDefault(p => p.Id == id);

        //    if (profile == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    UserProfileViewModel userProfile = new UserProfileViewModel()
        //    {
        //        Id = profile.Id,
        //        UserId = profile.UserId,
        //        OrganizationId = profile.OrganizationId,
        //        Default = profile.Default,
        //        ProfileName = profile.ProfileName,
        //        Title = profile.Title,
        //        FirstName = profile.FirstName,
        //        MiddleName = profile.MiddleName,
        //        LastName = profile.LastName,
        //        Nickname = profile.Nickname,
        //        SecondaryEmail = profile.SecondaryEmail,
        //        SecondaryPhoneNumber = profile.SecondaryPhoneNumber,
        //        TimeZone = profile.TimeZone,
        //        DstAdjust = profile.DstAdjust,
        //        Language = profile.Language                
        //    };

        //    return View(userProfile);
        //}

        //// GET: Users/ProfileCreate
        //public ActionResult Create(int id)
        //{
        //    UserProfileViewModel userProfileViewModel = new UserProfileViewModel();
        //    userProfileViewModel.UserId = (int)id;

        //    return View(userProfileViewModel);
        //}

        //// POST: Users/Profile/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,UserId,OrganizationId,Default,ProfileName,Title,FirstName," +
        //                                           "MiddleName,LastName,Nickname,SecondaryEmail,SecondaryPhoneNumber," +
        //                                           "TimeZone,DstAdjust,Language")]
        //    UserProfileViewModel userProfileViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        UserProfile userProfile = new UserProfile()
        //        {
        //            Id = userProfileViewModel.Id,
        //            UserId = userProfileViewModel.UserId,
        //            OrganizationId = userProfileViewModel.OrganizationId,
        //            Default = userProfileViewModel.Default,
        //            ProfileName = userProfileViewModel.ProfileName,
        //            Title = userProfileViewModel.Title,
        //            FirstName = userProfileViewModel.FirstName,
        //            MiddleName = userProfileViewModel.MiddleName,
        //            LastName = userProfileViewModel.LastName,
        //            Nickname = userProfileViewModel.Nickname,
        //            SecondaryEmail = userProfileViewModel.SecondaryEmail,
        //            SecondaryPhoneNumber = userProfileViewModel.SecondaryPhoneNumber,
        //            TimeZone = userProfileViewModel.TimeZone,
        //            DstAdjust = userProfileViewModel.DstAdjust,
        //            Language = userProfileViewModel.Language,
        //        };

        //        _profileRepository.InsertOrUpdate(userProfile);
        //        _coreUnitOfWork.Save();

        //        return RedirectToAction("Index", new RouteValueDictionary(
        //            new { controller = "User", action = "ProfileIndex", Id = userProfile.UserId }));
        //    }

        //    return View(userProfileViewModel);
        //}


        ////GET
        //public ActionResult Edit(int id)
        //{
        //    UserProfile profile = _profileRepository.Find(id);

        //    if (profile == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    UserProfileViewModel userProfile = new UserProfileViewModel()
        //    {
        //        Id = profile.Id,
        //        UserId = profile.UserId,
        //        OrganizationId = profile.OrganizationId,
        //        Default = profile.Default,
        //        ProfileName = profile.ProfileName,
        //        Title = profile.Title,
        //        FirstName = profile.FirstName,
        //        MiddleName = profile.MiddleName,
        //        LastName = profile.LastName,
        //        Nickname = profile.Nickname,
        //        SecondaryEmail = profile.SecondaryEmail,
        //        SecondaryPhoneNumber = profile.SecondaryPhoneNumber,
        //        TimeZone = profile.TimeZone,
        //        DstAdjust = profile.DstAdjust,
        //        Language = profile.Language
        //    };

        //    return View(userProfile);
        //}

        ////POST  /Users/ProfileEdit/
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,UserId,OrganizationId,Default,ProfileName,Title,FirstName," +
        //                                         "MiddleName,LastName,Nickname,SecondaryEmail,SecondaryPhoneNumber," +
        //                                         "TimeZone,DstAdjust,Language")]
        //    UserProfileViewModel userProfileViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        UserProfile profile = _profileRepository.Find(userProfileViewModel.Id);

        //        profile.Id = userProfileViewModel.Id;
        //        profile.UserId = userProfileViewModel.UserId;
        //        profile.OrganizationId = userProfileViewModel.OrganizationId;
        //        profile.Default = userProfileViewModel.Default;
        //        profile.ProfileName = userProfileViewModel.ProfileName;
        //        profile.Title = userProfileViewModel.Title;
        //        profile.FirstName = userProfileViewModel.FirstName;
        //        profile.MiddleName = userProfileViewModel.MiddleName;
        //        profile.LastName = userProfileViewModel.LastName;
        //        profile.Nickname = userProfileViewModel.Nickname;
        //        profile.SecondaryEmail = userProfileViewModel.SecondaryEmail;
        //        profile.SecondaryPhoneNumber = userProfileViewModel.SecondaryPhoneNumber;
        //        profile.TimeZone = userProfileViewModel.TimeZone;
        //        profile.DstAdjust = userProfileViewModel.DstAdjust;
        //        profile.Language = userProfileViewModel.Language;

        //        _profileRepository.InsertOrUpdate(profile);
        //        _coreUnitOfWork.Save();

        //        return RedirectToAction("Index", new RouteValueDictionary(
        //            new {controller = "User", action = "UserProfileIndex", Id = profile.UserId}));
        //    }

        //    return View(userProfileViewModel);
        //}

        //// GET: UserProfile/Delete/5
        //public ActionResult Delete(int? id)

        //{
        //    UserProfile profile = _profileRepository.Find((int) id);

        //    if (profile == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    UserProfileViewModel userProfileViewModel = new UserProfileViewModel()
        //    {
        //        Id = profile.Id,
        //        UserId = profile.UserId,
        //        OrganizationId = profile.OrganizationId,
        //        Default = profile.Default,
        //        ProfileName = profile.ProfileName,
        //        Title = profile.Title,
        //        FirstName = profile.FirstName,
        //        MiddleName = profile.MiddleName,
        //        LastName = profile.LastName,
        //        Nickname = profile.Nickname,
        //        SecondaryEmail = profile.SecondaryEmail,
        //        SecondaryPhoneNumber = profile.SecondaryPhoneNumber,
        //        TimeZone = profile.TimeZone,
        //        DstAdjust = profile.DstAdjust,
        //        Language = profile.Language
        //    };

        //    return View(userProfileViewModel);
        //}

        //// POST: UserProfile/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    int userId = _profileRepository.Find(id).UserId;
        //    _profileRepository.Delete(id);
        //    _coreUnitOfWork.Save();

        //    return RedirectToAction("Index", new { id = userId });
        //}

    }
}