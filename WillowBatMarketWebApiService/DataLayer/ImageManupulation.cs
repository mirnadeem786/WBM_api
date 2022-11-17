using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.DataLayer
{
    public interface IimageManupulation
    {
        public string getImageByItemId(Guid itemId);
 public ResponseModel insertImage(ItemImages item);


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
            string imagepath = path + "/" + itemId + ".png";                                      // + Path.GetExtension(file.FileName);
            byte[] b = System.IO.File.ReadAllBytes(imagepath);

            return "data:image/png;base64," + Convert.ToBase64String(b);

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








    }
}
