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
        private readonly ZONERadarRepository _ZONERadarRepository;
        public MyCollectionService()
        {
            _ZONERadarRepository = new ZONERadarRepository();
        }
        public MyCollectionViewModel GetMemCollection(int? memberId) 
        {
            //初始化
            var resultMemCollection = new MyCollectionViewModel
            {
                User = new User() ,
                CollectionSpaces = new List<MyCollectionSpaces>()
            };
            //抓出使用者 內容
            var u = _ZONERadarRepository.GetAll<Member>().FirstOrDefault(x=>x.MemberID == memberId);
            if (u == null)
            {
                return resultMemCollection;
            }
            else
            {
                resultMemCollection.User = new User
                {
                    Name = u.Name,
                    Description = u.Description,
                    Email = u.Email,
                    Phone = u.Phone,
                    Photo = u.Photo,
                    SignUpDateTime = u.SignUpDateTime
                };
                //改使用者的Collection

                var collection = _ZONERadarRepository.GetAll<Collection>().Where(x=>x.MemberID == memberId);
                var spaces = _ZONERadarRepository.GetAll<Space>();
                var re = _ZONERadarRepository.GetAll<Review>();
                if (collection == null)
                {
                    return resultMemCollection;
                }
                else { 
                    foreach (var c in collection)
                    {
                        var temp = new MyCollectionSpaces
                        {
                            SpaceName = GetSpace(spaces, c).SpaceName,
                            Address = GetSpace(spaces, c).Address,
                            City = GetSpace(spaces,c).City.CityName,
                            Districts = GetSpace(spaces, c).District.DistrictName,
                            Photo = GetSpace(spaces, c).SpacePhoto.First().SpacePhotoUrl,
                            PricePerHour = GetSpace(spaces, c).PricePerHour,
                            ReviewCount = re.Where(x => x.Order.SpaceID == c.SpaceID).Select(x => x.Score).Count()
                        };
                        resultMemCollection.CollectionSpaces.Add(temp);
                    }
                }
            }
            return resultMemCollection;
        }

        private static Space GetSpace(IQueryable<Space> spaces, Collection c)
        {
            return spaces.FirstOrDefault(x => x.SpaceID == c.SpaceID);
        }

    }
}