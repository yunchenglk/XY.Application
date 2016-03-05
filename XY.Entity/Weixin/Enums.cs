using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity.Weixin
{
    /// <summary>
    /// 接收消息类型
    /// </summary>
    public enum RequestMsgType
    {
        TEXT, //文本
        LOCATION, //地理位置
        IMAGE, //图片
        VOICE, //语音
        VIDEO, //视频
        LINK, //连接信息
        EVENT, //事件推送
        SHORTVIDEO,//小视频
    }


    /// <summary>
    /// 当RequestMsgType类型为Event时，Event属性的类型
    /// </summary>
    public enum Event
    {
        /// <summary>
        /// 进入会话（似乎已从官方API中移除）
        /// </summary>
        ENTER,

        /// <summary>
        /// 地理位置（似乎已从官方API中移除）
        /// </summary>
        LOCATION,

        /// <summary>
        /// 订阅
        /// </summary>
        SUBSCRIBE,

        /// <summary>
        /// 取消订阅
        /// </summary>
        UNSUBSCRIBE,

        /// <summary>
        /// 自定义菜单点击事件
        /// </summary>
        CLICK,

        /// <summary>
        /// 二维码扫描
        /// </summary>
        SCAN,

        /// <summary>
        /// URL跳转
        /// </summary>
        VIEW,

        /// <summary>
        /// 事件推送群发结果
        /// </summary>
        MASSSENDJOBFINISH,

        /// <summary>
        /// 模板信息发送完成
        /// </summary>
        TEMPLATESENDJOBFINISH,

        /// <summary>
        /// 扫码推事件
        /// </summary>
        SCANCODE_PUSH,

        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        SCANCODE_WAITMSG,

        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        PIC_SYSPHOTO,

        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        PIC_PHOTO_OR_ALBUM,

        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        PIC_WEIXIN,

        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        LOCATION_SELECT,

        /// <summary>
        /// 卡券通过审核
        /// </summary>
        CARD_PASS_CHECK,

        /// <summary>
        /// 卡券未通过审核
        /// </summary>
        CARD_NOT_PASS_CHECK,

        /// <summary>
        /// 领取卡券
        /// </summary>
        USER_GET_CARD,

        /// <summary>
        /// 删除卡券
        /// </summary>
        USER_DEL_CARD,

        /// <summary>
        /// 多客服接入会话
        /// </summary>
        KF_CREATE_SESSION,

        /// <summary>
        /// 多客服关闭会话
        /// </summary>
        KF_CLOSE_SESSION,

        /// <summary>
        /// 多客服转接会话
        /// </summary>
        KF_SWITCH_SESSION,

        /// <summary>
        /// 审核结果事件推送
        /// </summary>
        POI_CHECK_NOTIFY,

        /// <summary>
        /// Wi-Fi连网成功
        /// </summary>
        WIFICONNECTED,

        /// <summary>
        /// 卡券核销
        /// </summary>
        USER_CONSUME_CARD,

        /// <summary>
        /// 进入会员卡
        /// </summary>
        USER_VIEW_CARD,

        /// <summary>
        /// 从卡券进入公众号会话
        /// </summary>
        USER_ENTER_SESSION_FROM_CARD,

        /// <summary>
        /// 微小店订单付款通知
        /// </summary>
        MERCHANT_ORDER,

        /// <summary>
        /// 接收会员信息事件通知
        /// </summary>
        SUBMIT_MEMBERCARD_USER_INFO,

        /// <summary>
        /// 摇一摇事件通知
        /// </summary>
        SHAKEAROUNDUSERSHAKE,
    }


    /// <summary>
    /// 发送消息类型
    /// </summary>
    public enum ResponseMsgType
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text = 0,
        /// <summary>
        /// 单图文
        /// </summary>
        News = 1,
        /// <summary>
        /// 音乐
        /// </summary>
        Music = 2,
        /// <summary>
        /// 图片
        /// </summary>
        Image = 3,
        /// <summary>
        /// 语音
        /// </summary>
        Voice = 4,
        /// <summary>
        /// 视频
        /// </summary>
        Video = 5,
        /// <summary>
        /// 多客服
        /// </summary>
        Transfer_Customer_Service,

        //以下为延伸类型，微信官方并未提供具体的回复类型
        /// <summary>
        /// 多图文
        /// </summary>
        MultipleNews = 106,
        /// <summary>
        /// 位置
        /// </summary>
        LocationMessage = 107,
        /// <summary>
        /// 无回复
        /// </summary>
        NoResponse = 110,
    }

    /// <summary>
    /// 菜单按钮类型
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 点击
        /// </summary>
        click,
        /// <summary>
        /// Url
        /// </summary>
        view,
        /// <summary>
        /// 扫码推事件
        /// </summary>
        scancode_push,
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        scancode_waitmsg,
        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        pic_sysphoto,
        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        pic_photo_or_album,
        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        pic_weixin,
        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        location_select
    }

    /// <summary>
    /// 上传媒体文件类型
    /// </summary>
    public enum UploadMediaFileType
    {
        /// <summary>
        /// 图片: 128K，支持JPG格式
        /// </summary>
        image,
        /// <summary>
        /// 语音：256K，播放长度不超过60s，支持AMR\MP3格式
        /// </summary>
        voice,
        /// <summary>
        /// 视频：1MB，支持MP4格式
        /// </summary>
        video,
        /// <summary>
        /// thumb：64KB，支持JPG格式
        /// </summary>
        thumb,
        /// <summary>
        /// 图文消息
        /// </summary>
        news
    }


    ///// <summary>
    ///// 群发消息返回状态
    ///// </summary>
    //public enum GroupMessageStatus
    //{
    //    //高级群发消息的状态
    //    涉嫌广告 = 10001,
    //    涉嫌政治 = 20001,
    //    涉嫌社会 = 20004,
    //    涉嫌色情 = 20002,
    //    涉嫌违法犯罪 = 20006,
    //    涉嫌欺诈 = 20008,
    //    涉嫌版权 = 20013,
    //    涉嫌互推 = 22000,
    //    涉嫌其他 = 21000
    //}
    public enum TenPayV3Type
    {
        JSAPI,
        NATIVE,
        APP
    }

    public enum GroupMessageType
    {
        /// <summary>
        /// 图文消息
        /// </summary>
        mpnews = 0,
        /// <summary>
        /// 文本
        /// </summary>
        text = 1,
        /// <summary>
        /// 语音
        /// </summary>
        voice = 2,
        /// <summary>
        /// 图片
        /// </summary>
        image = 3,
        /// <summary>
        /// 视频
        /// </summary>
        video = 4
    }
    /// <summary>
    /// 卡券类型
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// 通用券
        /// </summary>
        GENERAL_COUPON = 0,
        /// <summary>
        /// 团购券
        /// </summary>
        GROUPON = 1,
        /// <summary>
        /// 折扣券
        /// </summary>
        DISCOUNT = 2,
        /// <summary>
        /// 礼品券
        /// </summary>
        GIFT = 3,
        /// <summary>
        /// 代金券
        /// </summary>
        CASH = 4,
        /// <summary>
        /// 会员卡
        /// </summary>
        MEMBER_CARD = 5,
        /// <summary>
        /// 门票
        /// </summary>
        SCENIC_TICKET = 6,
        /// <summary>
        /// 电影票
        /// </summary>
        MOVIE_TICKET = 7,
        /// <summary>
        /// 飞机票
        /// </summary>
        BOARDING_PASS = 8,
        /// <summary>
        /// 红包
        /// </summary>
        LUCKY_MONEY = 9,
    }
    /// <summary>
    /// 卡券code码展示类型
    /// </summary>
    public enum Card_CodeType
    {
        /// <summary>
        /// 文本
        /// </summary>
        CODE_TYPE_TEXT = 0,
        /// <summary>
        /// 一维码
        /// </summary>
        CODE_TYPE_BARCODE = 1,
        /// <summary>
        /// 二维码
        /// </summary>
        CODE_TYPE_QRCODE = 2,
    }
    /// <summary>
    /// 卡券 商户自定义cell 名称
    /// </summary>
    public enum Card_UrlNameType
    {
        /// <summary>
        /// 外卖
        /// </summary>
        URL_NAME_TYPE_TAKE_AWAY = 0,
        /// <summary>
        /// 在线预订
        /// </summary>
        URL_NAME_TYPE_RESERVATION = 1,
        /// <summary>
        /// 立即使用
        /// </summary>
        URL_NAME_TYPE_USE_IMMEDIATELY = 2,
        /// <summary>
        /// 在线预约
        /// </summary>
        URL_NAME_TYPE_APPOINTMENT = 3,
        /// <summary>
        /// 在线兑换
        /// </summary>
        URL_NAME_TYPE_EXCHANGE = 4,
        /// <summary>
        /// 车辆信息
        /// </summary>
        URL_NAME_TYPE_VEHICLE_INFORMATION = 5,
    }
    public enum ServiceType
    {
        订阅号 = 0,
        由历史老帐号升级后的订阅号 = 1,
        服务号 = 2
    }
    /// <summary>
    /// 授权方认证类型
    /// </summary>
    public enum VerifyType
    {
        未认证 = -1,
        微信认证 = 0,
        新浪微博认证 = 1,
        腾讯微博认证 = 2,
        已资质认证通过但还未通过名称认证 = 3,
        已资质认证通过还未通过名称认证但通过了新浪微博认证 = 4,
        已资质认证通过还未通过名称认证但通过了腾讯微博认证 = 5
    }
    /// <summary>
    /// 公众号授权给开发者的权限集列表
    /// </summary>
    public enum FuncscopeCategory
    {
        消息与菜单权限集 = 1,
        用户管理权限集 = 2,
        帐号管理权限集 = 3,
        网页授权权限集 = 4,
        微信小店权限集 = 5,
        多客服权限集 = 6,
        业务通知权限集 = 7,
        微信卡券权限集 = 8,
        素材管理权限集 = 9,
        摇一摇周边权限集 = 10,
        线下门店权限集 = 11,
        微信连WIFI权限集 = 12,
        未知类型 = 13
    }
    /// <summary>
    /// 选项设置信息选项名称
    /// </summary>
    public enum OptionName
    {
        /// <summary>
        /// 地理位置上报选项
        /// 0	无上报
        /// 1	进入会话时上报
        /// 2	每5s上报
        /// </summary>
        location_report,
        /// <summary>
        /// 语音识别开关选项
        /// 0	关闭语音识别
        /// 1	开启语音识别
        /// </summary>
        voice_recognize,
        /// <summary>
        /// 客服开关选项
        /// 0	关闭多客服
        /// 1	开启多客服
        /// </summary>
        customer_service
    }

    /// <summary>
    /// 应用授权作用域
    /// </summary>
    public enum OAuthScope
    {
        /// <summary>
        /// 不弹出授权页面，直接跳转，只能获取用户openid
        /// </summary>
        snsapi_base,
        /// <summary>
        /// 弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息
        /// </summary>
        snsapi_userinfo,
        /// <summary>
        /// 网站应用授权登录
        /// </summary>
        snsapi_login,
    }
    /// <summary>
    /// 企业接收消息类型
    /// </summary>
    public enum QyRequestMsgType
    {
        Text, //文本
        Location, //地理位置
        Image, //图片
        Voice, //语音
        Video, //视频
        Link, //连接信息
        Event, //事件推送
    }
    /// <summary>
    /// 返回码（JSON）
    /// </summary>
    public enum ReturnCode
    {
        系统繁忙 = -1,
        请求成功 = 0,
        验证失败 = 40001,
        不合法的凭证类型 = 40002,
        不合法的OpenID = 40003,
        不合法的媒体文件类型 = 40004,
        不合法的文件类型 = 40005,
        不合法的文件大小 = 40006,
        不合法的媒体文件id = 40007,
        不合法的消息类型 = 40008,
        不合法的图片文件大小 = 40009,
        不合法的语音文件大小 = 40010,
        不合法的视频文件大小 = 40011,
        不合法的缩略图文件大小 = 40012,
        不合法的APPID = 40013,
        //不合法的access_token      =             40014,
        不合法的access_token = 40014,
        不合法的菜单类型 = 40015,
        //不合法的按钮个数             =          40016,
        //不合法的按钮个数              =         40017,
        不合法的按钮个数1 = 40016,
        不合法的按钮个数2 = 40017,
        不合法的按钮名字长度 = 40018,
        不合法的按钮KEY长度 = 40019,
        不合法的按钮URL长度 = 40020,
        不合法的菜单版本号 = 40021,
        不合法的子菜单级数 = 40022,
        不合法的子菜单按钮个数 = 40023,
        不合法的子菜单按钮类型 = 40024,
        不合法的子菜单按钮名字长度 = 40025,
        不合法的子菜单按钮KEY长度 = 40026,
        不合法的子菜单按钮URL长度 = 40027,
        不合法的自定义菜单使用用户 = 40028,
        不合法的oauth_code = 40029,
        不合法的refresh_token = 40030,
        缺少access_token参数 = 41001,
        缺少appid参数 = 41002,
        缺少refresh_token参数 = 41003,
        缺少secret参数 = 41004,
        缺少多媒体文件数据 = 41005,
        缺少media_id参数 = 41006,
        缺少子菜单数据 = 41007,
        access_token超时 = 42001,
        需要GET请求 = 43001,
        需要POST请求 = 43002,
        需要HTTPS请求 = 43003,
        多媒体文件为空 = 44001,
        POST的数据包为空 = 44002,
        图文消息内容为空 = 44003,
        多媒体文件大小超过限制 = 45001,
        消息内容超过限制 = 45002,
        标题字段超过限制 = 45003,
        描述字段超过限制 = 45004,
        链接字段超过限制 = 45005,
        图片链接字段超过限制 = 45006,
        语音播放时间超过限制 = 45007,
        图文消息超过限制 = 45008,
        接口调用超过限制 = 45009,
        创建菜单个数超过限制 = 45010,
        不存在媒体数据 = 46001,
        不存在的菜单版本 = 46002,
        不存在的菜单数据 = 46003,
        解析JSON_XML内容错误 = 47001,
        api功能未授权 = 48001,
        用户未授权该api = 50001,

        //新加入的一些类型，以下文字根据P2P项目格式组织，非官方文字
        发送消息失败_48小时内用户未互动 = 10706,
        发送消息失败_该用户已被加入黑名单_无法向此发送消息 = 62751,
        发送消息失败_对方关闭了接收消息 = 10703,
        对方不是粉丝 = 10700
    }
    /// <summary>
    /// 语言
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        zh_CN,
        /// <summary>
        /// 中文繁体
        /// </summary>
        zh_TW,
        /// <summary>
        /// 英文
        /// </summary>
        en
    }
    /// <summary>
    /// AppStore状态
    /// </summary>
    public enum AppStoreState
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None = 1,
        /// <summary>
        /// 已进入应用状态
        /// </summary>
        Enter = 2,
        /// <summary>
        /// 退出App状态（临时传输状态，退出后即为None）
        /// </summary>
        Exit = 4
    }
    /// <summary>
    /// 自动回复规则类型
    /// </summary>
    public enum AutoReplyType
    {
        /// <summary>
        /// 文本
        /// </summary>
        text = 0,
        /// <summary>
        /// 图片
        /// </summary>
        img = 1,
        /// <summary>
        /// 语音
        /// </summary>
        voice = 2,
        /// <summary>
        /// 视频
        /// </summary>
        video = 3,
        /// <summary>
        /// 图文消息
        /// </summary>
        news = 4,
    }

    /// <summary>
    /// 自动回复模式
    /// </summary>
    public enum AutoReplyMode
    {
        /// <summary>
        /// 全部回复
        /// </summary>
        reply_all = 0,
        /// <summary>
        /// 随机回复其中一条
        /// </summary>
        random_one = 1,
    }

    /// <summary>
    /// 自动回复匹配模式
    /// </summary>
    public enum AutoReplyMatchMode
    {
        /// <summary>
        /// 消息中含有该关键词即可
        /// </summary>
        contain = 0,
        /// <summary>
        /// 消息内容必须和关键词严格相同
        /// </summary>
        equal = 1,
    }
}
