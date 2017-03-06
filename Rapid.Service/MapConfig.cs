using AutoMapper;
using Rapid.Model.Models;
using Rapid.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Service
{
    public static class MapConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Employee, EmployeeViewModel>()
                .ForMember(t => t.EmployeeId, opt => opt.MapFrom(s => s.Id))
                .ForMember(t => t.LastName, opt => opt.MapFrom(s => splitName(s.EmployeeName)[0]))
                .ForMember(t => t.FirstName, opt => opt.MapFrom(s => splitName(s.EmployeeName)[1]))
                .ForMember(t => t.NickName, opt => opt.Ignore());

            Mapper.CreateMap<EmployeeViewModel, Employee>()
                .ForMember(t => t.Id, opt => opt.MapFrom(s => s.EmployeeId))
                .ForMember(t => t.EmployeeName, opt => opt.MapFrom(s => combineName(s.LastName, s.FirstName)))
                .ForSourceMember(t => t.NickName, opt => opt.Ignore());
        }

        internal static string combineName(string lastName, string firstName)
        {
            return string.Format("{0}, {1}", lastName, firstName);
        }

        internal static List<string> splitName(string fullName)
        {
            var names = fullName.Split(',').Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t.Trim()).ToList();
            if (names.Count == 0)
            {
                names.Add(string.Empty);
            }
            if (names.Count == 1)
            {
                names.Add(string.Empty);
            }
            return names;
        }

    }
}
