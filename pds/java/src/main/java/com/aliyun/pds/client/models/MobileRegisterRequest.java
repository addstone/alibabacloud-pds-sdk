// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.pds.client.models;

import com.aliyun.tea.*;

/**
 * 
 */
public class MobileRegisterRequest extends TeaModel {
    @NameInMap("headers")
    public java.util.Map<String, String> headers;

    // App ID, 当前访问的App
    @NameInMap("app_id")
    @Validation(required = true)
    public String appId;

    // 待查询的手机号
    @NameInMap("phone_number")
    @Validation(required = true)
    public String phoneNumber;

    // 国家编号，默认86，不需要填+号，直接填数字
    @NameInMap("phone_region")
    public String phoneRegion;

    // 短信验证码内容
    @NameInMap("sms_code")
    @Validation(required = true)
    public String smsCode;

    // 短信验证码ID
    @NameInMap("sms_code_id")
    @Validation(required = true)
    public String smsCodeId;

    public static MobileRegisterRequest build(java.util.Map<String, ?> map) throws Exception {
        MobileRegisterRequest self = new MobileRegisterRequest();
        return TeaModel.build(map, self);
    }

    public MobileRegisterRequest setHeaders(java.util.Map<String, String> headers) {
        this.headers = headers;
        return this;
    }
    public java.util.Map<String, String> getHeaders() {
        return this.headers;
    }

    public MobileRegisterRequest setAppId(String appId) {
        this.appId = appId;
        return this;
    }
    public String getAppId() {
        return this.appId;
    }

    public MobileRegisterRequest setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
        return this;
    }
    public String getPhoneNumber() {
        return this.phoneNumber;
    }

    public MobileRegisterRequest setPhoneRegion(String phoneRegion) {
        this.phoneRegion = phoneRegion;
        return this;
    }
    public String getPhoneRegion() {
        return this.phoneRegion;
    }

    public MobileRegisterRequest setSmsCode(String smsCode) {
        this.smsCode = smsCode;
        return this;
    }
    public String getSmsCode() {
        return this.smsCode;
    }

    public MobileRegisterRequest setSmsCodeId(String smsCodeId) {
        this.smsCodeId = smsCodeId;
        return this;
    }
    public String getSmsCodeId() {
        return this.smsCodeId;
    }

}
