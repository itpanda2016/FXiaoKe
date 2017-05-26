using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FROST.Utility;
using Newtonsoft.Json;

namespace FxkApi {
    public class ContainerAccessToken {
        public static AccessTokenReturn GetCorpAccessToken(string appId = "FSAID_13170e5",
            string appSecret = "66e6225aafc84504b88c161e5390116a", string permanentCode = "934BB296915679720794B0060F26CE90") {
            string url = "https://open.fxiaoke.com/cgi/corpAccessToken/get/V2";
            AccessTokenModel model = new AccessTokenModel() {
                appId = "FSAID_13170e5",
                appSecret = "66e6225aafc84504b88c161e5390116a",
                permanentCode = "934BB296915679720794B0060F26CE90"      //永久授权码
            };
            return JsonConvert.DeserializeObject<AccessTokenReturn>(General.CurlByDotNet(url, CurlMethod.POST, JsonConvert.SerializeObject(model)));
        }
    }
    public class AccessTokenModel {
        public string appId { set; get; }
        public string appSecret { set; get; }
        public string permanentCode { set; get; }
    }

    public class AccessTokenReturn {
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        public string corpAccessToken { get; set; }
        public string corpId { get; set; }
        public int expiresIn { get; set; }
    }

}
