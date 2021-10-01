using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;

namespace ZoneRadar.Services
{
    public class HostInfoService
    {
        private readonly ZONERadarRepository _zoneradarRepository;
        public HostInfoService()
        {
            _zoneradarRepository = new ZONERadarRepository();
        }
        public HostInfoViewModel GetMemberSpace(int? memberId)
        {

            var resultMember = new HostInfoViewModel
            {
                User = new User(),
                Spaces = new List<Spaces>()
            };
            //int memberId = 1;
            var u = _zoneradarRepository.GetAll<Member>().FirstOrDefault(x => x.MemberID == memberId);
            if (u == null)
            {
                return resultMember;
            }
            else
            {
                resultMember.User = new User
                {
                    MemberId = u.MemberID,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    Description = u.Description,
                    SignUpDateTime = u.SignUpDateTime,
                    Photo = u.Photo
                };
                //會員所擁有的廠所有場地
                var sps = _zoneradarRepository.GetAll<Space>().Where(x => x.MemberID == memberId);
                var re = _zoneradarRepository.GetAll<Review>();

                foreach (var s in sps)
                {
                    var temp = new Spaces
                    {
                        SpaceName = s.SpaceName,
                        Address = s.Address,
                        Photo = sps.FirstOrDefault(x=>x.SpaceID == s.SpaceID).SpacePhoto.First().SpacePhotoUrl
                        /*sps.FirstOrDefault(x=>x.SpacePhoto.Select(spp=> spp.SpaceID).First() == s.SpaceID).SpacePhoto.First().SpacePhotoUrl*/
                        /*sps.FirstOrDefault(x => x.SpaceID == s.SpaceID).SpacePhoto.FirstOrDefault(x=>x.SpaceID == s.SpaceID).SpacePhotoUrl*/
                        /*spt.FirstOrDefault(x => x.SpaceID == s.SpaceID).SpacePhotoUrl*/,
                        Districts = sps.FirstOrDefault(x=>x.District.DistrictID == s.DistrictID ).District.DistrictName
                        /*sps.Where(x => x.DistrictID == s.District.DistrictID).Select(x => x.District.DistrictName).FirstOrDefault()*/,
                        City = sps.FirstOrDefault(x=>x.City.CityID == s.CityID).City.CityName,
                        PricePerHour = s.PricePerHour,
                        ReviewCount = re.Where(x => x.Order.SpaceID == s.SpaceID).Select(x => x.Score).Count()
                    };
                    resultMember.Spaces.Add(temp);
                }
                return resultMember;
            }
        }
    }
}
