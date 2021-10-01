using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;

namespace ZoneRadar.Services
{
    public class MyCollectionService
    {
        private readonly ZONERadarRepository _zoneradarRepository;
        public MyCollectionService()
        {
            _zoneradarRepository = new ZONERadarRepository();
        }
        public MyCollectionViewModel GetMemberCollection(int? memberId)
        {
            var resultMember = new MyCollectionViewModel
            {
                User = new User(),
                MyCollection = new List<Spaces>()
            };
            var u = _zoneradarRepository.GetAll<Member>().FirstOrDefault(x => x.MemberID == memberId);
            if (u == null)
            {
                return resultMember;
            }
            else
            {
                resultMember.User = new User
                {
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    Description = u.Description,
                    SignUpDateTime = u.SignUpDateTime,
                    Photo = u.Photo
                };
                //會員所擁有的廠所有場地
                var sps = _zoneradarRepository.GetAll<Space>().Where(x => x.MemberID == memberId);
                var city = _zoneradarRepository.GetAll<City>();
                var district = _zoneradarRepository.GetAll<District>();
                var re = _zoneradarRepository.GetAll<Review>();
                var spt = _zoneradarRepository.GetAll<SpacePhoto>();

                foreach (var s in sps)
                {
                    var temp = new Spaces
                    {
                        SpaceName = s.SpaceName,
                        Address = s.Address,
                        SpacePhoto = sps.FirstOrDefault(x => x.SpaceID == s.SpaceID).SpacePhoto.First().SpacePhotoUrl,
                        District = district.FirstOrDefault(x => x.DistrictID == s.DistrictID).DistrictName,
                        City = city.FirstOrDefault(x => x.CityID == s.CityID).CityName,
                        PricePerHour = s.PricePerHour,
                        ReviewCount = re.Where(x => x.Order.MemberID == s.MemberID && x.ToHost == true).Select(x => x.Score).Count()
                    };
                    resultMember.MyCollection.Add(temp);
                }

                return resultMember;
            }
        }
    }
}