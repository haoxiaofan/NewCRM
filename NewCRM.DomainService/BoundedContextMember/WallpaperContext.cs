﻿using System;
using System.Collections.Generic;
using System.Linq;
using NewCRM.Domain.Entitys.System;
using NewCRM.Domain.Services.Interface;
using NewCRM.Infrastructure.CommonTools.CustomException;
using NewCRM.Infrastructure.CommonTools.CustomExtension;
using NewCRM.Repository.StorageProvider;

namespace NewCRM.Domain.Services.BoundedContextMember
{
    public class WallpaperContext : BaseServiceContext, IWallpaperContext
    {
        public Tuple<int, string> AddWallpaper(Wallpaper wallpaper)
        {
            ValidateParameter.Validate(wallpaper);
            using(var dataStore = new DataStore())
            {
                #region 前置条件验证
                {
                    var sql = $@"
SELECT COUNT(*) FROM dbo.Wallpapers AS a WHERE a.AccountId={wallpaper.AccountId} AND a.IsDeleted=0";
                    var result = (Int32)dataStore.SqlScalar(sql);
                    if(result >= 6)
                    {
                        throw new BusinessException("最多只能上传6张图片");
                    }
                }
                #endregion

                var newWallpaperId = 0;
                #region 插入壁纸
                {
                    var sql = $@"INSERT dbo.Wallpapers
                            ( Title ,
                              Url ,
                              ShortUrl ,
                              Source ,
                              Description ,
                              Width ,
                              Height ,
                              AccountId ,
                              Md5 ,
                              IsDeleted ,
                              AddTime ,
                              LastModifyTime
                            )
                    VALUES  ( N'{wallpaper.Title}' , -- Title - nvarchar(max)
                              N'{wallpaper.Url}' , -- Url - nvarchar(max)
                              N'{wallpaper.ShortUrl}' , -- ShortUrl - nvarchar(max)
                              {(Int32)wallpaper.Source} , -- Source - int
                              N'{wallpaper.Description}' , -- Description - nvarchar(50)
                              {wallpaper.Width} , -- Width - int
                              {wallpaper.Height} , -- Height - int
                              {wallpaper.AccountId} , -- AccountId - int
                              N'{wallpaper.Md5}' , -- Md5 - nvarchar(max)
                              0 , -- IsDeleted - bit
                              GETDATE() , -- AddTime - datetime
                              GETDATE()  -- LastModifyTime - datetime
                            ) SELECT @@IDENTITY AS Identity";

                    newWallpaperId = (Int32)dataStore.SqlScalar(sql);
                }
                #endregion

                #region 获取返回值
                {
                    var sql = $@"
SELECT a.Id,a.Url FROM dbo.Wallpapers AS a WHERE a.Id={newWallpaperId} AND a.IsDeleted=0";

                    var result = dataStore.SqlGetDataReader(sql);
                    return new Tuple<int, string>(Int32.Parse(result["Id"].ToString()), result["Url"].ToString());
                }
                #endregion
            }
        }

        public Wallpaper GetUploadWallpaper(string md5)
        {
            throw new NotImplementedException();
        }

        public List<Wallpaper> GetUploadWallpaper(int accountId)
        {
            throw new NotImplementedException();
        }

        public List<Wallpaper> GetWallpapers()
        {
            using(var dataStore = new DataStore())
            {
                var sql = $@"SELECT
                            a.AccountId,
                            a.Height,
                            a.Id,
                            a.Md5,
                            a.ShortUrl,
                            a.Source,
                            a.Title,
                            a.Url,
                            a.Width
                            FROM dbo.Wallpapers AS a WHERE a.IsDeleted=0";
                return dataStore.SqlGetDataTable(sql).AsList<Wallpaper>().ToList();
            }
        }

        public void ModifyWallpaper(int accountId, int newWallpaperId)
        {
            throw new NotImplementedException();
        }

        public void ModifyWallpaperMode(int accountId, string newMode)
        {
            throw new NotImplementedException();
        }

        public void RemoveWallpaper(int accountId, int wallpaperId)
        {
            throw new NotImplementedException();
        }
    }
}
