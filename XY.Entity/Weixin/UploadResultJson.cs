using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity.Weixin
{
    /// <summary>
    /// 上传媒体文件返回结果
    /// </summary>
    public class UploadResultJson : WxJsonResult
    {
        public UploadMediaFileType type { get; set; }
        public string media_id { get; set; }
        public string thumb_media_id { get; set; } // 上传缩略图返回的meidia_id参数.
        public long created_at { get; set; }
    }


    /// <summary>
    /// 上传临时媒体文件返回结果
    /// </summary>
    public class UploadTemporaryMediaResult : WxJsonResult
    {
        public UploadMediaFileType type { get; set; }
        public string media_id { get; set; }
        /// <summary>
        /// 上传缩略图返回的meidia_id参数.
        /// </summary>
        public string thumb_media_id { get; set; }
        public long created_at { get; set; }
    }

    /// <summary>
    /// 上传永久媒体文件返回结果
    /// </summary>
    public class UploadForeverMediaResult : WxJsonResult
    {
        /// <summary>
        /// 新增的永久素材的media_id
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 新增的图片素材的图片URL（仅新增图片素材时会返回该字段）
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 上传图文消息内的图片获取URL返回结果
    /// </summary>
    public class UploadImgResult : WxJsonResult
    {
        public string url { get; set; }
    }


    /// <summary>
    /// 获取图文类型永久素材返回结果
    /// </summary>
    public class GetNewsResultJson : WxJsonResult
    {
        public List<ForeverNewsItem> news_item { get; set; }
    }

    public class ForeverNewsItem : NewsModel
    {
        /// <summary>
        /// 图文页的URL
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 获取素材总数返回结果
    /// </summary>
    public class GetMediaCountResultJson : WxJsonResult
    {
        /// <summary>
        /// 语音总数量
        /// </summary>
        public int voice_count { get; set; }
        /// <summary>
        /// 视频总数量
        /// </summary>
        public int video_count { get; set; }
        /// <summary>
        /// 图片总数量
        /// </summary>
        public int image_count { get; set; }
        /// <summary>
        /// 图文总数量
        /// </summary>
        public int news_count { get; set; }
    }
    /// <summary>
    /// 获取素材总数返回结果
    /// </summary>
    public class BaseMediaListResultJson : WxJsonResult
    {
        /// <summary>
        /// 该类型的素材的总数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 本次调用获取的素材的数量
        /// </summary>
        public int item_count { get; set; }
    }

    /// <summary>
    /// 图文素材的Item
    /// </summary>
    public class MediaList_NewsResult : BaseMediaListResultJson
    {
        public List<MediaList_News_Item> item { get; set; }
    }

    public class MediaList_News_Item
    {
        public string media_id { get; set; }
        public Media_News_Content content { get; set; }
        /// <summary>
        /// 这个素材的最后更新时间
        /// </summary>
        public long update_time { get; set; }
    }

    public class Media_News_Content
    {
        public List<Media_News_Content_Item> news_item { get; set; }
    }

    public class Media_News_Content_Item : NewsModel
    {
        public string url { get; set; }

        /// <summary>
        /// 封面图片的url
        /// </summary>
        public string thumb_url { get; set; }
    }

    /// <summary>
    /// 除图文之外的其他素材的Item
    /// </summary>
    public class MediaList_OthersResult : BaseMediaListResultJson
    {
        public List<MediaList_Others_Item> item { get; set; }
    }

    public class MediaList_Others_Item
    {
        public string media_id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 这个素材的最后更新时间
        /// </summary>
        public long update_time { get; set; }
        /// <summary>
        /// 图文页的URL，或者，当获取的列表是图片素材列表时，该字段是图片的URL
        /// </summary>
        public string url { get; set; }
    }

}
