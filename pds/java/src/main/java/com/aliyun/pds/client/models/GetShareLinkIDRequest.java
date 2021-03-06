// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.pds.client.models;

import com.aliyun.tea.*;

/**
 * get_share_id request
 */
public class GetShareLinkIDRequest extends TeaModel {
    @NameInMap("headers")
    public java.util.Map<String, String> headers;

    // share_msg
    @NameInMap("share_msg")
    public String shareMsg;

    public static GetShareLinkIDRequest build(java.util.Map<String, ?> map) throws Exception {
        GetShareLinkIDRequest self = new GetShareLinkIDRequest();
        return TeaModel.build(map, self);
    }

    public GetShareLinkIDRequest setHeaders(java.util.Map<String, String> headers) {
        this.headers = headers;
        return this;
    }
    public java.util.Map<String, String> getHeaders() {
        return this.headers;
    }

    public GetShareLinkIDRequest setShareMsg(String shareMsg) {
        this.shareMsg = shareMsg;
        return this;
    }
    public String getShareMsg() {
        return this.shareMsg;
    }

}
