using Microsoft.Practices.Unity;
using Model.Core;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using System;

namespace RepositoryNH
{
    public class Repository
    {
        public Repository(IUnityContainer container)
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(db =>
               {
                   db.ConnectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=NHibernateTest; Trusted_Connection=true";
                   db.Dialect<MsSql2008Dialect>();
                   db.BatchSize = 500;
               });
            cfg.AddAssembly("RepositoryNH");
            cfg.SetProperty("hbm2ddl.keywords", "auto-quote");

            //cfg.SetProperty("cache.use_second_level_cache", "true");
            //cfg.SetProperty("cache.default_expiration", "60000");
            //cfg.SetProperty("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            //cfg.SetProperty("connection.isolation", "ReadCommitted");
            
            var intercepter = new UnityDataBindingIntercepter(container);
            cfg.SetInterceptor(intercepter);

            cfg.SetProperty("max_fetch_depth", "10");


            var sessionFactory = cfg.BuildSessionFactory();

            intercepter.SessionFactory = sessionFactory;

            new SchemaExport(cfg).Create(true, true);


            Guid item_guid = Guid.NewGuid();

            Guid bid1_guid = Guid.NewGuid();
            Guid bid2_guid = Guid.NewGuid();

            Guid bidDetail1_guid = Guid.NewGuid();
            Guid bidDetail2_guid = Guid.NewGuid();
            Guid bidDetail3_guid = Guid.NewGuid();
            Guid bidDetail4_guid = Guid.NewGuid();
            Guid bidDetail5_guid = Guid.NewGuid();
            Guid bidDetail6_guid = Guid.NewGuid();
            Guid bidDetail7_guid = Guid.NewGuid();
            Guid bidDetail8_guid = Guid.NewGuid();

            Guid bidInnerDetail1_guid = Guid.NewGuid();
            Guid bidInnerDetail2_guid = Guid.NewGuid();
            Guid bidInnerDetail3_guid = Guid.NewGuid();
            Guid bidInnerDetail4_guid = Guid.NewGuid();
            Guid bidInnerDetail5_guid = Guid.NewGuid();
            Guid bidInnerDetail6_guid = Guid.NewGuid();
            Guid bidInnerDetail7_guid = Guid.NewGuid();
            Guid bidInnerDetail8_guid = Guid.NewGuid();
            Guid bidInnerDetail9_guid = Guid.NewGuid();
            Guid bidInnerDetail10_guid = Guid.NewGuid();
            Guid bidInnerDetail11_guid = Guid.NewGuid();
            Guid bidInnerDetail12_guid = Guid.NewGuid();
            Guid bidInnerDetail13_guid = Guid.NewGuid();
            Guid bidInnerDetail14_guid = Guid.NewGuid();
            Guid bidInnerDetail15_guid = Guid.NewGuid();
            Guid bidInnerDetail16_guid = Guid.NewGuid();

            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    // item
                    Item item = container.Resolve<Item>();
                    item.Id = item_guid;
                    item.ItemDescription = "ItemNote";

                    // bid1
                    Bid bid1 = new Bid();
                    bid1.Id = bid1_guid;
                    bid1.Description = "Bid1Description";
                    bid1.Item = item;

                    // bid2
                    Bid bid2 = new Bid();
                    bid2.Id = bid2_guid;
                    bid2.Description = "Bid2Description";
                    bid2.Item = item;

                    // bidDetail1
                    BidDetail bidDetail1 = new BidDetail();
                    bidDetail1.Id = bidDetail1_guid;
                    bidDetail1.BidDetailDescription = "bidDetail1";
                    bidDetail1.Bid = bid1;

                    // bidDetail2
                    BidDetail bidDetail2 = new BidDetail();
                    bidDetail2.Id = bidDetail2_guid;
                    bidDetail2.BidDetailDescription = "bidDetail2";
                    bidDetail2.Bid = bid1;

                    // bidDetail3
                    BidDetail bidDetail3 = new BidDetail();
                    bidDetail3.Id = bidDetail3_guid;
                    bidDetail3.BidDetailDescription = "bidDetail3";
                    bidDetail3.Bid = bid1;

                    // bidDetail4
                    BidDetail bidDetail4 = new BidDetail();
                    bidDetail4.Id = bidDetail4_guid;
                    bidDetail4.BidDetailDescription = "bidDetail4";
                    bidDetail4.Bid = bid1;

                    // bidDetail5
                    BidDetail bidDetail5 = new BidDetail();
                    bidDetail5.Id = bidDetail5_guid;
                    bidDetail5.BidDetailDescription = "bidDetail5";
                    bidDetail5.Bid = bid2;

                    // bidDetail6
                    BidDetail bidDetail6 = new BidDetail();
                    bidDetail6.Id = bidDetail6_guid;
                    bidDetail6.BidDetailDescription = "bidDetail6";
                    bidDetail6.Bid = bid2;

                    // bidDetail7
                    BidDetail bidDetail7 = new BidDetail();
                    bidDetail7.Id = bidDetail7_guid;
                    bidDetail7.BidDetailDescription = "bidDetail7";
                    bidDetail7.Bid = bid2;

                    // bidDetail8
                    BidDetail bidDetail8 = new BidDetail();
                    bidDetail8.Id = bidDetail8_guid;
                    bidDetail8.BidDetailDescription = "bidDetail8";
                    bidDetail8.Bid = bid2;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail1 = new BidInnerDetail();
                    bidInnerDetail1.Id = bidInnerDetail1_guid;
                    bidInnerDetail1.BidInnerDetailDescription = "bidInnerDetail1";
                    bidInnerDetail1.BidDetail = bidDetail1;

                    // bidInnerDetail2
                    BidInnerDetail bidInnerDetail2 = new BidInnerDetail();
                    bidInnerDetail2.Id = bidInnerDetail2_guid;
                    bidInnerDetail2.BidInnerDetailDescription = "bidInnerDetail2";
                    bidInnerDetail2.BidDetail = bidDetail1;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail3 = new BidInnerDetail();
                    bidInnerDetail3.Id = bidInnerDetail3_guid;
                    bidInnerDetail3.BidInnerDetailDescription = "bidInnerDetail3";
                    bidInnerDetail3.BidDetail = bidDetail2;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail4 = new BidInnerDetail();
                    bidInnerDetail4.Id = bidInnerDetail4_guid;
                    bidInnerDetail4.BidInnerDetailDescription = "bidInnerDetail4";
                    bidInnerDetail4.BidDetail = bidDetail2;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail5 = new BidInnerDetail();
                    bidInnerDetail5.Id = bidInnerDetail5_guid;
                    bidInnerDetail5.BidInnerDetailDescription = "bidInnerDetail5";
                    bidInnerDetail5.BidDetail = bidDetail3;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail6 = new BidInnerDetail();
                    bidInnerDetail6.Id = bidInnerDetail6_guid;
                    bidInnerDetail6.BidInnerDetailDescription = "bidInnerDetail6";
                    bidInnerDetail6.BidDetail = bidDetail3;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail7 = new BidInnerDetail();
                    bidInnerDetail7.Id = bidInnerDetail7_guid;
                    bidInnerDetail7.BidInnerDetailDescription = "bidInnerDetail7";
                    bidInnerDetail7.BidDetail = bidDetail4;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail8 = new BidInnerDetail();
                    bidInnerDetail8.Id = bidInnerDetail8_guid;
                    bidInnerDetail8.BidInnerDetailDescription = "bidInnerDetail8";
                    bidInnerDetail8.BidDetail = bidDetail4;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail9 = new BidInnerDetail();
                    bidInnerDetail9.Id = bidInnerDetail9_guid;
                    bidInnerDetail9.BidInnerDetailDescription = "bidInnerDetail9";
                    bidInnerDetail9.BidDetail = bidDetail5;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail10 = new BidInnerDetail();
                    bidInnerDetail10.Id = bidInnerDetail10_guid;
                    bidInnerDetail10.BidInnerDetailDescription = "bidInnerDetail10";
                    bidInnerDetail10.BidDetail = bidDetail5;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail11 = new BidInnerDetail();
                    bidInnerDetail11.Id = bidInnerDetail11_guid;
                    bidInnerDetail11.BidInnerDetailDescription = "bidInnerDetail11";
                    bidInnerDetail11.BidDetail = bidDetail6;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail12 = new BidInnerDetail();
                    bidInnerDetail12.Id = bidInnerDetail12_guid;
                    bidInnerDetail12.BidInnerDetailDescription = "bidInnerDetail12";
                    bidInnerDetail12.BidDetail = bidDetail6;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail13 = new BidInnerDetail();
                    bidInnerDetail13.Id = bidInnerDetail13_guid;
                    bidInnerDetail13.BidInnerDetailDescription = "bidInnerDetail13";
                    bidInnerDetail13.BidDetail = bidDetail7;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail14 = new BidInnerDetail();
                    bidInnerDetail14.Id = bidInnerDetail14_guid;
                    bidInnerDetail14.BidInnerDetailDescription = "bidInnerDetail14";
                    bidInnerDetail14.BidDetail = bidDetail7;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail15 = new BidInnerDetail();
                    bidInnerDetail15.Id = bidInnerDetail15_guid;
                    bidInnerDetail15.BidInnerDetailDescription = "bidInnerDetail15";
                    bidInnerDetail15.BidDetail = bidDetail8;

                    // bidInnerDetail1
                    BidInnerDetail bidInnerDetail16 = new BidInnerDetail();
                    bidInnerDetail16.Id = bidInnerDetail16_guid;
                    bidInnerDetail16.BidInnerDetailDescription = "bidInnerDetail16";
                    bidInnerDetail16.BidDetail = bidDetail8;

                    item.AddBid(bid1);

                    item.AddBid(bid2);

                    bid1.AddBidDetail(bidDetail1);
                    bid1.AddBidDetail(bidDetail2);
                    bid1.AddBidDetail(bidDetail3);
                    bid1.AddBidDetail(bidDetail4);

                    bid2.AddBidDetail(bidDetail5);
                    bid2.AddBidDetail(bidDetail6);
                    bid2.AddBidDetail(bidDetail7);
                    bid2.AddBidDetail(bidDetail8);

                    bidDetail1.AddBidInnerDetail(bidInnerDetail1);
                    bidDetail1.AddBidInnerDetail(bidInnerDetail2);

                    bidDetail2.AddBidInnerDetail(bidInnerDetail3);
                    bidDetail2.AddBidInnerDetail(bidInnerDetail4);

                    bidDetail3.AddBidInnerDetail(bidInnerDetail5);
                    bidDetail3.AddBidInnerDetail(bidInnerDetail6);

                    bidDetail4.AddBidInnerDetail(bidInnerDetail7);
                    bidDetail4.AddBidInnerDetail(bidInnerDetail8);

                    bidDetail5.AddBidInnerDetail(bidInnerDetail9);
                    bidDetail6.AddBidInnerDetail(bidInnerDetail10);

                    bidDetail6.AddBidInnerDetail(bidInnerDetail11);
                    bidDetail6.AddBidInnerDetail(bidInnerDetail2);

                    bidDetail7.AddBidInnerDetail(bidInnerDetail3);
                    bidDetail7.AddBidInnerDetail(bidInnerDetail4);

                    bidDetail8.AddBidInnerDetail(bidInnerDetail5);
                    bidDetail8.AddBidInnerDetail(bidInnerDetail6);


                    var xxx = session.Save("Item", item);
                    var yyy = session.Save(bid1);

                    session.Save(bid1);
                    session.Save(bid2);

                    session.Save(bidDetail1);
                    session.Save(bidDetail2);
                    session.Save(bidDetail3);
                    session.Save(bidDetail4);
                    session.Save(bidDetail5);
                    session.Save(bidDetail6);
                    session.Save(bidDetail7);
                    session.Save(bidDetail8);

                    session.Save(bidInnerDetail1);
                    session.Save(bidInnerDetail2);
                    session.Save(bidInnerDetail3);
                    session.Save(bidInnerDetail4);
                    session.Save(bidInnerDetail5);
                    session.Save(bidInnerDetail6);
                    session.Save(bidInnerDetail7);
                    session.Save(bidInnerDetail8);
                    session.Save(bidInnerDetail9);
                    session.Save(bidInnerDetail10);
                    session.Save(bidInnerDetail11);
                    session.Save(bidInnerDetail12);
                    session.Save(bidInnerDetail13);
                    session.Save(bidInnerDetail14);
                    session.Save(bidInnerDetail15);
                    session.Save(bidInnerDetail16);

                    item.ToString();

                    tx.Commit();


                }
                //using (var tx = session.BeginTransaction())
                //{
                //    session.Get<Item>(item_guid);
                //    tx.Commit();
                //}
            }

            Item itemX;

            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    itemX = session.Get<Item>(item_guid);
                }
            }

            Console.WriteLine(itemX.ToString());
        }
    }
}
