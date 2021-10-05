using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;

namespace ZoneRadar.Services
{
    public class MemberService
    {
        private readonly ZONERadarRepository _zoneradarRepository;
        private readonly ZONERadarRepository _repository;

        public MemberService()
        {
            _repository = new ZONERadarRepository();
            _zoneradarRepository = new ZONERadarRepository();
        }

        //HostInfo
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
                var spt = _zoneradarRepository.GetAll<SpacePhoto>();

                foreach (var s in sps)
                {
                    resultMember.Spaces.Add(new Spaces
                    {
                        SpaceName = s.SpaceName,
                        Address = s.Address,
                        SpacePhoto = sps.FirstOrDefault(x => x.SpaceID == s.SpaceID).SpacePhoto.First().SpacePhotoUrl,
                        District = sps.FirstOrDefault(x => x.SpaceID == s.SpaceID).District.DistrictName,
                        City = sps.FirstOrDefault(x => x.SpaceID == s.SpaceID).City.CityName,
                        PricePerHour = s.PricePerHour,
                        ReviewCount = re.Where(x => x.Order.MemberID == s.MemberID && x.ToHost == true).Select(x => x.Score).Count()
                    });
                }
                return resultMember;
            }
        }
        //MyCollection
        public MyCollectionViewModel GetMemberCollection(int? memberId)
        {
            var resultMemberCollection = new MyCollectionViewModel
            {
                User = new User(),
                MyCollection = new List<Spaces>()
            };

            var u = _zoneradarRepository.GetAll<Member>().FirstOrDefault(x => x.MemberID == memberId);
            if (u == null)
            {
                return resultMemberCollection;
            }
            else
            {
                resultMemberCollection.User = new User
                {
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    Description = u.Description,
                    SignUpDateTime = u.SignUpDateTime,
                    Photo = u.Photo
                };
                //會員所收藏的場地
                var collection = _zoneradarRepository.GetAll<Collection>().Where(x => x.MemberID == memberId);
                var sps = _zoneradarRepository.GetAll<Space>();

                foreach (var c in collection)
                {
                    resultMemberCollection.MyCollection.Add(new Spaces
                    {
                        SpaceName = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).SpaceName,
                        Address = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).Address,
                        SpacePhoto = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).SpacePhoto.First().SpacePhotoUrl,
                        District = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).District.DistrictName,
                        City = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).City.CityName,
                        PricePerHour = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).PricePerHour,
                        ReviewCount = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).Order.Select(x => x.Review).Count()
                        /*re.Where(x => x.Order.MemberID == s.MemberID && x.ToHost == true).Select(x => x.Score).Count()*/
                    });
                }
                return resultMemberCollection;
            }

        }
        //HostReview
        public UserInfoViewModel GetHostReview(int? memberId)
        {
            var resulthostinfoReview = new UserInfoViewModel
            {
                User = new User(),
                ToUserReview = new List<UserReview>()
            };

            var u = _zoneradarRepository.GetAll<Member>().FirstOrDefault(x => x.MemberID == memberId);
            if (u == null)
            {
                return resulthostinfoReview;
            }
            else
            {
                //找出會員
                resulthostinfoReview.User = new User
                {
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    Description = u.Description,
                    SignUpDateTime = u.SignUpDateTime,
                    Photo = u.Photo
                };
                //找出會員是否有租借場地並且顯示 出被場地主的評價
                var order = _zoneradarRepository.GetAll<Order>().Where(x => x.MemberID == u.MemberID && x.OrderStatusID == 4);
                var sps = _zoneradarRepository.GetAll<Space>();
                foreach (var o in order)
                {
                    resulthostinfoReview.ToUserReview.Add(new UserReview
                    {
                        SpaceName = sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).SpaceName,
                        SpacePhoto = sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).Member.Photo,
                        District = sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).District.DistrictName,
                        Address = sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).Address,
                        PricePerHour = sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).PricePerHour,
                        ReviewContent = o.Review.FirstOrDefault(x => x.OrderID == o.OrderID && x.ToHost == false).ReviewContent,
                        Recommend = o.Review.FirstOrDefault(x => x.OrderID == o.OrderID && x.ToHost == false).Recommend,
                        Score = o.Review.FirstOrDefault(x => x.OrderID == o.OrderID && x.ToHost == false).Score,
                        ReviewDate = o.Review.FirstOrDefault(x => x.OrderID == o.OrderID && x.ToHost == false).ReviewDate,
                        ReviewCount = o.Review.Where(x => x.OrderID == o.OrderID && x.ToHost == false).Count()
                    });

                    return resulthostinfoReview;
                }
                return resulthostinfoReview;
            }
        }
    }
}