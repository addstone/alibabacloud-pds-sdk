// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace Aliyun.SDK.PDS.Client.Models
{
    /**
     * Get story detail request
     */
    public class GetStoryDetailRequest : TeaModel {
        [NameInMap("headers")]
        [Validation(Required=false)]
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// drive_id
        /// </summary>
        [NameInMap("drive_id")]
        [Validation(Required=true, Pattern="[0-9]+")]
        public string DriveId { get; set; }

        /// <summary>
        /// story_id
        /// </summary>
        [NameInMap("story_id")]
        [Validation(Required=true)]
        public string StoryId { get; set; }

        /// <summary>
        /// url_expire_sec
        /// </summary>
        [NameInMap("video_url_expire_sec")]
        [Validation(Required=false)]
        public long VideoUrlExpireSec { get; set; }

    }

}
