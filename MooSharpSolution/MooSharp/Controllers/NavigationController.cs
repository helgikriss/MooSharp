﻿using Microsoft.AspNet.Identity;
using MooSharp.Models.ViewModels.Students;
using MooSharp.Models.ViewModels.Teachers;
using MooSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooSharp.Controllers
{
    public class NavigationController : Controller
    {
        private CoursesService _coursesService = new CoursesService();

		[ChildActionOnly]
		public ActionResult SideBar() {
			if (User.IsInRole("Administrators")) {
				return PartialView("AdminSideBar");
			}
			if (User.IsInRole("Teachers")) {
				var viewModel = new TeacherIndexViewModel() {
					MyCourses = _coursesService.GetCoursesByUser(User.Identity.GetUserId())
				};
				return PartialView("TeacherSideBar", viewModel);
			}
            if (User.IsInRole("Students")) {
                var viewModel = new StudentIndexViewModel()
                {
                    MyCourses = _coursesService.GetCoursesByUser(User.Identity.GetUserId())
                };

                return PartialView("StudentSideBar", viewModel);
            }
			return new EmptyResult();
		}
	}
}