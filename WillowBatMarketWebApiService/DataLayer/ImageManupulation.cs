﻿using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using WillowBatMarketWebApiService.Models;
using System.Collections.Generic;
using System.Collections;
using System.Resources;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WillowBatMarketWebApiService.DataLayer
{
    public interface IimageManupulation
    {
        public string getImageByItemId(Guid itemId);
 public ResponseModel insertImage(ItemImages item);
        public ResponseModel fetchImages(Pagination pagination, string imageType);
        public bool deleteImage(Guid itemId,string imageType);  


    }



    public class ImageManupulation:IimageManupulation
    {

        public ImageManupulation() { }

        private readonly IWebHostEnvironment webHostEnvironment;
        ResponseModel responseModel;
        public ImageManupulation(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            responseModel=new ResponseModel();
        }

        public string getImageByItemId(Guid itemId)

        {
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "~/Uploads/bat/");
            string imagepath = path + "/" + itemId + ".png";
            // + Path.GetExtension(file.FileName);
            try
            {
                byte[] b = System.IO.File.ReadAllBytes(imagepath);
                return "data:image/png;base64," + Convert.ToBase64String(b);
            }
            catch(Exception e)
            {
                
                return null;

            }

        }
        public  ResponseModel insertImage(ItemImages item)
        {
            ItemImages itemImage = new ItemImages();
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "~/Uploads/" + item.imageType + "/");
            // file.(path);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);

            }

            string imagepath = path + "/" + item.itemId + ".png";



            //+Path.GetExtension(file.FileName);
            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);

            }


            byte[] img = Convert.FromBase64String(item.base64image.Substring((item.base64image.LastIndexOf(',') + 1)));
            System.IO.File.WriteAllBytes(imagepath, img);

            try
            {
               /* // itemImage.imageName = file.FileName;
                itemImage.imageType = EntityType.BAT;
                itemImage.itemId = item.itemId;
                itemImage.imageId = Guid.NewGuid();
                itemImage.imagePath = imagepath;
                _appDbContext.Add(itemImage);
                _appDbContext.SaveChanges();
                 using (FileStream fileStream = System.IO.File.Create(imagepath))
                  {

                     // file.CopyTo(fileStream);

                  }*/
            }
            catch (Exception e)
            {
                responseModel.Success = false;
                responseModel.Message = "unable to upload image";
                return responseModel;
            }
            responseModel.Message = "image uploaded";
            return responseModel;
        }

        public ResponseModel fetchImages(Pagination pagination,string imageType)
        {

            Image image;

            DirectoryInfo directory = new DirectoryInfo(Path.Combine(webHostEnvironment.ContentRootPath, "~/Uploads/" + imageType + "/"));
          IEnumerable  <FileInfo> files = directory.GetFiles().AsEnumerable().OrderBy(o=>o.Name)
        .Skip((pagination.PageNumber - 1) * pagination.PageSize)
        .Take(pagination.PageSize);
            ArrayList list=new ArrayList();
            foreach (FileInfo file in files)
            {
                image = new Image();
                if (file.Extension == ".png")
                {


                    try
                    {
                        image.imageUrl = file.FullName;
                        image.imageName=file.Name;
                        byte[] b = System.IO.File.ReadAllBytes(file.FullName);
                        image.image = "data:image/png;base64," + Convert.ToBase64String(b);
                        list.Add(image);
                    }
                    catch (Exception e)
                    {

                        return null;

                    }

                   

                }
            }
                if(list.Count<0)
                {
              
                    responseModel.Success = false;

                    return responseModel;

                }
            responseModel.TotalRecords = list.Count;
            responseModel.Data = list;
              
                return responseModel;

            }

        public bool deleteImage(Guid itemId,string imageType)

        {

            var path = Path.Combine(webHostEnvironment.ContentRootPath, "~/Uploads/" + imageType + "/");
            // file.(path);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);

            }

            string imagepath = path + "/" + itemId + ".png";



            //+Path.GetExtension(file.FileName);
            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
                return true;
            }
            return false;



        }
    }
    }

