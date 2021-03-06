// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Tea;
using Tea.Utils;

using Aliyun.SDK.Hosting.Client.Models;

namespace Aliyun.SDK.Hosting.Client
{
    public class Client 
    {
        protected string _domainId;
        protected AlibabaCloud.PDS.Credential.Credential _accessTokenCredential;
        protected string _endpoint;
        protected string _protocol;
        protected string _nickname;
        protected string _userAgent;
        protected Aliyun.Credentials.Client _credential;

        public Client(Config config)
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(config.ToMap()))
            {
                throw new TeaException(new Dictionary<string, string>
                {
                    {"name", "ParameterMissing"},
                    {"message", "'config' can not be unset"},
                });
            }
            AlibabaCloud.TeaUtil.Common.ValidateModel(config);
            if (!AlibabaCloud.TeaUtil.Common.Empty(config.AccessToken) || !AlibabaCloud.TeaUtil.Common.Empty(config.RefreshToken))
            {
                AlibabaCloud.PDS.Credential.Models.Config accessConfig = new AlibabaCloud.PDS.Credential.Models.Config
                {
                    AccessToken = config.AccessToken,
                    Endpoint = config.Endpoint,
                    DomainId = config.DomainId,
                    ClientId = config.ClientId,
                    RefreshToken = config.RefreshToken,
                    ClientSecret = config.ClientSecret,
                    ExpireTime = config.ExpireTime,
                };
                this._accessTokenCredential = new AlibabaCloud.PDS.Credential.Credential(accessConfig);
            }
            if (!AlibabaCloud.TeaUtil.Common.Empty(config.AccessKeyId))
            {
                if (AlibabaCloud.TeaUtil.Common.Empty(config.Type))
                {
                    config.Type = "access_key";
                }
                Aliyun.Credentials.Models.Config credentialConfig = new Aliyun.Credentials.Models.Config
                {
                    AccessKeyId = config.AccessKeyId,
                    Type = config.Type,
                    AccessKeySecret = config.AccessKeySecret,
                    SecurityToken = config.SecurityToken,
                };
                this._credential = new Aliyun.Credentials.Client(credentialConfig);
            }
            this._endpoint = config.Endpoint;
            this._protocol = config.Protocol;
            this._userAgent = config.UserAgent;
            this._nickname = config.Nickname;
            this._domainId = config.DomainId;
        }

        /**
         * 取消绑定关系，生成新用户，返回访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public CancelLinkModel CancelLinkEx(CancelLinkRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/cancel_link");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CancelLinkModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 取消绑定关系，生成新用户，返回访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<CancelLinkModel> CancelLinkExAsync(CancelLinkRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/cancel_link");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CancelLinkModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 确认绑定关系, 成功后返回访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public ConfirmLinkModel ConfirmLinkEx(ConfirmLinkRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/confirm_link");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ConfirmLinkModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 确认绑定关系, 成功后返回访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<ConfirmLinkModel> ConfirmLinkExAsync(ConfirmLinkRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/confirm_link");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ConfirmLinkModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 修改手机登录密码，密码必须包含数字和字母，长度8-20个字符
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public ChangePasswordModel ChangePasswordEx(DefaultChangePasswordRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/default/change_password");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ChangePasswordModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 修改手机登录密码，密码必须包含数字和字母，长度8-20个字符
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<ChangePasswordModel> ChangePasswordExAsync(DefaultChangePasswordRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/default/change_password");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ChangePasswordModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 设置手机登录密码，密码必须包含数字和字母，长度8-20个字符
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public SetPasswordModel SetPasswordEx(DefaultSetPasswordRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/default/set_password");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<SetPasswordModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 设置手机登录密码，密码必须包含数字和字母，长度8-20个字符
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<SetPasswordModel> SetPasswordExAsync(DefaultSetPasswordRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/default/set_password");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<SetPasswordModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 校验手机短信验证码，用于重置密码时校验手机，通过校验后返回state，可通过state重新设置密码
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public VerifyCodeModel VerifyCodeEx(VerifyCodeRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/default/verify_code");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<VerifyCodeModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 校验手机短信验证码，用于重置密码时校验手机，通过校验后返回state，可通过state重新设置密码
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<VerifyCodeModel> VerifyCodeExAsync(VerifyCodeRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/default/verify_code");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<VerifyCodeModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 管理员通过账号信息直接获取用户的访问令牌
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public GetAccessTokenByLinkInfoModel GetAccessTokenByLinkInfoEx(GetAccessTokenByLinkInfoRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/get_access_token_by_link_info");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetAccessTokenByLinkInfoModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 管理员通过账号信息直接获取用户的访问令牌
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<GetAccessTokenByLinkInfoModel> GetAccessTokenByLinkInfoExAsync(GetAccessTokenByLinkInfoRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/get_access_token_by_link_info");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetAccessTokenByLinkInfoModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取图片验证码，用于人机校验，默认不需要
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public GetCaptchaModel GetCaptchaEx(GetCaptchaRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/get_captcha");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetCaptchaModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取图片验证码，用于人机校验，默认不需要
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<GetCaptchaModel> GetCaptchaExAsync(GetCaptchaRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/get_captcha");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetCaptchaModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取用户认证方式详情
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public GetLinkInfoModel GetLinkInfoEx(GetByLinkInfoRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/get_link_info");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetLinkInfoModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取用户认证方式详情
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<GetLinkInfoModel> GetLinkInfoExAsync(GetByLinkInfoRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/get_link_info");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetLinkInfoModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取用户的所有绑定信息
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public GetLinkInfoByUserIdModel GetLinkInfoByUserIdEx(GetLinkInfoByUserIDRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/get_link_info_by_user_id");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetLinkInfoByUserIdModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取用户的所有绑定信息
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<GetLinkInfoByUserIdModel> GetLinkInfoByUserIdExAsync(GetLinkInfoByUserIDRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/get_link_info_by_user_id");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetLinkInfoByUserIdModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取公钥，用于加密对称密钥
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public GetPublicKeyModel GetPublicKeyEx(GetAppPublicKeyRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/get_public_key");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetPublicKeyModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取公钥，用于加密对称密钥
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<GetPublicKeyModel> GetPublicKeyExAsync(GetAppPublicKeyRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/get_public_key");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetPublicKeyModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 绑定用户认证方式
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         */
        public LinkModel LinkEx(AccountLinkRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/link");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<LinkModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 绑定用户认证方式
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<LinkModel> LinkExAsync(AccountLinkRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/link");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<LinkModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 查询手机号是否已被注册
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public CheckExistModel CheckExistEx(MobileCheckExistRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/mobile/check_exist");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CheckExistModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 查询手机号是否已被注册
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<CheckExistModel> CheckExistExAsync(MobileCheckExistRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/mobile/check_exist");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CheckExistModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 通过手机号+短信或密码登录，返回刷新令牌和访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public LoginModel LoginEx(MobileLoginRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/mobile/login");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<LoginModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 通过手机号+短信或密码登录，返回刷新令牌和访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<LoginModel> LoginExAsync(MobileLoginRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/mobile/login");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<LoginModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 通过手机号+短信验证码注册账号
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         */
        public RegisterModel RegisterEx(MobileRegisterRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/mobile/register");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<RegisterModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 通过手机号+短信验证码注册账号
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<RegisterModel> RegisterExAsync(MobileRegisterRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/mobile/register");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<RegisterModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 发送短信验证码，用于登录、注册、修改密码、绑定等
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public MobileSendSmsCodeModel MobileSendSmsCodeEx(MobileSendSmsCodeRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/mobile/send_sms_code");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<MobileSendSmsCodeModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 发送短信验证码，用于登录、注册、修改密码、绑定等
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<MobileSendSmsCodeModel> MobileSendSmsCodeExAsync(MobileSendSmsCodeRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/mobile/send_sms_code");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<MobileSendSmsCodeModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 用户退出登录
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public AccountRevokeModel AccountRevokeEx(RevokeRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/revoke");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<AccountRevokeModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 用户退出登录
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<AccountRevokeModel> AccountRevokeExAsync(RevokeRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/revoke");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<AccountRevokeModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 用户通过刷新令牌（refresh_token）获取访问令牌（access_token）
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public AccountTokenModel AccountTokenEx(TokenRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/token");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<AccountTokenModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 用户通过刷新令牌（refresh_token）获取访问令牌（access_token）
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<AccountTokenModel> AccountTokenExAsync(TokenRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/account/token");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".auth.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<AccountTokenModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 列举Store列表
         * @tags admin
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public AdminListStoresModel AdminListStoresEx(AdminListStoresRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/domain/list_stores");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<AdminListStoresModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 列举Store列表
         * @tags admin
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<AdminListStoresModel> AdminListStoresExAsync(AdminListStoresRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/domain/list_stores");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<AdminListStoresModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取用户的accessToken
         * @tags admin
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         */
        public GetUserAccessTokenModel GetUserAccessTokenEx(GetUserAccessTokenRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/get_access_token");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetUserAccessTokenModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取用户的accessToken
         * @tags admin
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         */
        public async Task<GetUserAccessTokenModel> GetUserAccessTokenExAsync(GetUserAccessTokenRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/get_access_token");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetUserAccessTokenModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 支持normal和large两种drive，
         * large类型的drive用于文件数多的场景，不支持list操作，
         * 当drive的文件数量大于1亿时，建议使用large类型。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CreateDriveModel CreateDriveEx(CreateDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/create");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 201))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CreateDriveModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 支持normal和large两种drive，
         * large类型的drive用于文件数多的场景，不支持list操作，
         * 当drive的文件数量大于1亿时，建议使用large类型。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CreateDriveModel> CreateDriveExAsync(CreateDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/create");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 201))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CreateDriveModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 删除指定drive_id对应的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public DeleteDriveModel DeleteDriveEx(DeleteDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/delete");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<DeleteDriveModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 删除指定drive_id对应的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<DeleteDriveModel> DeleteDriveExAsync(DeleteDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/delete");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<DeleteDriveModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取指定drive_id对应的Drive详细信息。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetDriveModel GetDriveEx(GetDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/get");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetDriveModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取指定drive_id对应的Drive详细信息。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetDriveModel> GetDriveExAsync(GetDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/get");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetDriveModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 一个用户可拥有多个drive，在创建drive时通过参数指定是否为这个用户的默认drive，
         * 每个用户只能设置一个默认drive。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetDefaultDriveModel GetDefaultDriveEx(GetDefaultDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/get_default_drive");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetDefaultDriveModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 一个用户可拥有多个drive，在创建drive时通过参数指定是否为这个用户的默认drive，
         * 每个用户只能设置一个默认drive。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetDefaultDriveModel> GetDefaultDriveExAsync(GetDefaultDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/get_default_drive");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetDefaultDriveModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 管理员列举指定用户的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListDrivesModel ListDrivesEx(ListDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/list");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListDrivesModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 管理员列举指定用户的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListDrivesModel> ListDrivesExAsync(ListDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/list");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListDrivesModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 列举当前用户（访问令牌）的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListMyDrivesModel ListMyDrivesEx(ListMyDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/list_my_drives");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListMyDrivesModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 列举当前用户（访问令牌）的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListMyDrivesModel> ListMyDrivesExAsync(ListMyDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/list_my_drives");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListMyDrivesModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 更新指定drive_id的Drive信息
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public UpdateDriveModel UpdateDriveEx(UpdateDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/update");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<UpdateDriveModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 更新指定drive_id的Drive信息
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<UpdateDriveModel> UpdateDriveExAsync(UpdateDriveRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/drive/update");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<UpdateDriveModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 完成文件上传
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CompleteFileModel CompleteFileEx(HostingCompleteFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/complete");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CompleteFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 完成文件上传
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CompleteFileModel> CompleteFileExAsync(HostingCompleteFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/complete");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CompleteFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定源文件或文件夹路径，拷贝到指定的文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CopyFileModel CopyFileEx(HostingCopyFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/copy");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 201))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CopyFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定源文件或文件夹路径，拷贝到指定的文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CopyFileModel> CopyFileExAsync(HostingCopyFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/copy");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 201))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CopyFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 创建文件或者文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CreateFileModel CreateFileEx(HostingCreateFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/create");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 201))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CreateFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 创建文件或者文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CreateFileModel> CreateFileExAsync(HostingCreateFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/create");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 201))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CreateFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定文件或文件夹路径，删除文件或文件夹
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public DeleteFileModel DeleteFileEx(HostingDeleteFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/delete");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<DeleteFileModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定文件或文件夹路径，删除文件或文件夹
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<DeleteFileModel> DeleteFileExAsync(HostingDeleteFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/delete");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<DeleteFileModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取指定文件或文件夹路径，获取文件或文件夹信息。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetFileModel GetFileEx(HostingGetFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/get");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取指定文件或文件夹路径，获取文件或文件夹信息。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetFileModel> GetFileExAsync(HostingGetFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/get");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定文件路径，获取文件下载地址
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetDownloadUrlModel GetDownloadUrlEx(HostingGetDownloadUrlRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/get_download_url");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetDownloadUrlModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定文件路径，获取文件下载地址
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetDownloadUrlModel> GetDownloadUrlExAsync(HostingGetDownloadUrlRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/get_download_url");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetDownloadUrlModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定文件路径，获取文件安全地址
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetSecureUrlModel GetSecureUrlEx(HostingGetSecureUrlRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/get_secure_url");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetSecureUrlModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定文件路径，获取文件安全地址
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetSecureUrlModel> GetSecureUrlExAsync(HostingGetSecureUrlRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/get_secure_url");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetSecureUrlModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 可指定分片信息，一次获取多个分片的上传地址。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetUploadUrlModel GetUploadUrlEx(HostingGetUploadUrlRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/get_upload_url");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetUploadUrlModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 可指定分片信息，一次获取多个分片的上传地址。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetUploadUrlModel> GetUploadUrlExAsync(HostingGetUploadUrlRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/get_upload_url");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetUploadUrlModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定父文件夹路径，列举文件夹下的文件或者文件夹
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListFileModel ListFileEx(HostingListFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/list");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定父文件夹路径，列举文件夹下的文件或者文件夹
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListFileModel> ListFileExAsync(HostingListFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/list");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 列举UploadID对应的已上传分片。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListUploadedPartsModel ListUploadedPartsEx(HostingListUploadedPartRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/list_uploaded_parts");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListUploadedPartsModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 列举UploadID对应的已上传分片。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListUploadedPartsModel> ListUploadedPartsExAsync(HostingListUploadedPartRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/list_uploaded_parts");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListUploadedPartsModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定源文件或文件夹路径，移动到指定的文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public MoveFileModel MoveFileEx(HostingMoveFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/move");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<MoveFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 指定源文件或文件夹路径，移动到指定的文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<MoveFileModel> MoveFileExAsync(HostingMoveFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/move");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<MoveFileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取视频支持的分辨率
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public VideoDefinitionModel VideoDefinitionEx(HostingVideoDefinitionRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/video_definition");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<VideoDefinitionModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取视频支持的分辨率
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<VideoDefinitionModel> VideoDefinitionExAsync(HostingVideoDefinitionRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/video_definition");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<VideoDefinitionModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取视频的DRM License
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public VideoLicenseModel VideoLicenseEx(HostingVideoDRMLicenseRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/video_license");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<VideoLicenseModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取视频的DRM License
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<VideoLicenseModel> VideoLicenseExAsync(HostingVideoDRMLicenseRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/video_license");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<VideoLicenseModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取视频转码后的m3u8文件
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public VideoM3u8Model VideoM3u8Ex(HostingVideoM3U8Request request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/video_m3u8");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        byte[] byt = AlibabaCloud.TeaUtil.Common.ReadAsBytes(response_.Body);
                        return TeaModel.ToObject<VideoM3u8Model>(new Dictionary<string, object>
                        {
                            {"body", byt},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取视频转码后的m3u8文件
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<VideoM3u8Model> VideoM3u8ExAsync(HostingVideoM3U8Request request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/video_m3u8");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        byte[] byt = AlibabaCloud.TeaUtil.Common.ReadAsBytes(response_.Body);
                        return TeaModel.ToObject<VideoM3u8Model>(new Dictionary<string, object>
                        {
                            {"body", byt},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 将mp4格式的视频文件，转为m3u8
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public VideoTranscodeModel VideoTranscodeEx(HostingVideoTranscodeRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/video_transcode");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<VideoTranscodeModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<VideoTranscodeModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 将mp4格式的视频文件，转为m3u8
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<VideoTranscodeModel> VideoTranscodeExAsync(HostingVideoTranscodeRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/file/video_transcode");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<VideoTranscodeModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<VideoTranscodeModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 创建共享。
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CreateShareModel CreateShareEx(CreateShareRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/share/create");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 201))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CreateShareModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 创建共享。
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CreateShareModel> CreateShareExAsync(CreateShareRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/share/create");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 201))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CreateShareModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 删除指定共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public DeleteShareModel DeleteShareEx(DeleteShareRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/share/delete");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<DeleteShareModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 删除指定共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<DeleteShareModel> DeleteShareExAsync(DeleteShareRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/share/delete");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<DeleteShareModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取共享信息。
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetShareModel GetShareEx(GetShareRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/share/get");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetShareModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取共享信息。
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetShareModel> GetShareExAsync(GetShareRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/share/get");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetShareModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 列举指定用户的共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListShareModel ListShareEx(ListShareRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/share/list");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListShareModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 列举指定用户的共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListShareModel> ListShareExAsync(ListShareRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/share/list");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListShareModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 修改指定共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public UpdateShareModel UpdateShareEx(UpdateShareRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/share/update");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<UpdateShareModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 修改指定共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<UpdateShareModel> UpdateShareExAsync(UpdateShareRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/share/update");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<UpdateShareModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 列举指定store下的所有文件。
         * @tags store
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListStorefileModel ListStorefileEx(ListStoreFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/store_file/list");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListStorefileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 列举指定store下的所有文件。
         * @tags store
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListStorefileModel> ListStorefileExAsync(ListStoreFileRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/hosting/store_file/list");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListStorefileModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 创建用户，只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CreateUserModel CreateUserEx(CreateUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/create");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 201))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CreateUserModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 创建用户，只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CreateUserModel> CreateUserExAsync(CreateUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/create");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 201))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<CreateUserModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public DeleteUserModel DeleteUserEx(DeleteUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/delete");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<DeleteUserModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<DeleteUserModel> DeleteUserExAsync(DeleteUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/delete");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return TeaModel.ToObject<DeleteUserModel>(new Dictionary<string, Dictionary<string, string>>
                        {
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取用户详细信息，普通用户只能获取自己的信息，管理员可以获取任意用户的信息。
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetUserModel GetUserEx(GetUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/get");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetUserModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 获取用户详细信息，普通用户只能获取自己的信息，管理员可以获取任意用户的信息。
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetUserModel> GetUserExAsync(GetUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/get");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<GetUserModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListUsersModel ListUsersEx(ListUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/list");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListUsersModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListUsersModel> ListUsersExAsync(ListUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/list");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<ListUsersModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 该接口将会根据条件查询用户，只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public SearchUserModel SearchUserEx(SearchUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/search");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<SearchUserModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 该接口将会根据条件查询用户，只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<SearchUserModel> SearchUserExAsync(SearchUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/search");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<SearchUserModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 用户可以修改自己的description，nick_name，avatar；
         * 管理员在用户基础上还可修改status（可以修改任意用户）；
         * 超级管理员在管理员基础上还可修改role（可以修改任意用户）。
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public UpdateUserModel UpdateUserEx(UpdateUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = GetAccessKeyId();
                    string accessKeySecret = GetAccessKeySecret();
                    string securityToken = GetSecurityToken();
                    string accessToken = GetAccessToken();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/update");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<UpdateUserModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 用户可以修改自己的description，nick_name，avatar；
         * 管理员在用户基础上还可修改status（可以修改任意用户）；
         * 超级管理员在管理员基础上还可修改role（可以修改任意用户）。
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<UpdateUserModel> UpdateUserExAsync(UpdateUserRequest request, RuntimeOptions runtime)
        {
            request.Validate();
            runtime.Validate();
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", runtime.ReadTimeout},
                {"connectTimeout", runtime.ConnectTimeout},
                {"localAddr", runtime.LocalAddr},
                {"httpProxy", runtime.HttpProxy},
                {"httpsProxy", runtime.HttpsProxy},
                {"noProxy", runtime.NoProxy},
                {"maxIdleConns", runtime.MaxIdleConns},
                {"socks5Proxy", runtime.Socks5Proxy},
                {"socks5NetWork", runtime.Socks5NetWork},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    string accesskeyId = await GetAccessKeyIdAsync();
                    string accessKeySecret = await GetAccessKeySecretAsync();
                    string securityToken = await GetSecurityTokenAsync();
                    string accessToken = await GetAccessTokenAsync();
                    Dictionary<string, object> realReq = AlibabaCloud.TeaUtil.Common.ToMap(request);
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, "https");
                    request_.Method = "POST";
                    request_.Pathname = GetPathname(_nickname, "/v2/user/update");
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"user-agent", GetUserAgent()},
                            {"host", AlibabaCloud.TeaUtil.Common.DefaultString(_endpoint, _domainId + ".api.aliyunpds.com")},
                            {"content-type", "application/json; charset=utf-8"},
                        },
                        request.Headers
                    );
                    realReq["headers"] = null;
                    if (!AlibabaCloud.TeaUtil.Common.Empty(accessToken))
                    {
                        request_.Headers["authorization"] = "Bearer " + accessToken;
                    }
                    else if (!AlibabaCloud.TeaUtil.Common.Empty(accesskeyId) && !AlibabaCloud.TeaUtil.Common.Empty(accessKeySecret))
                    {
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        request_.Headers["date"] = AlibabaCloud.TeaUtil.Common.GetDateUTCString();
                        request_.Headers["accept"] = "application/json";
                        request_.Headers["x-acs-signature-method"] = "HMAC-SHA1";
                        request_.Headers["x-acs-signature-version"] = "1.0";
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accesskeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(realReq));
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    Dictionary<string, object> respMap = null;
                    object obj = null;
                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 200))
                    {
                        obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                        respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                        return TeaModel.ToObject<UpdateUserModel>(new Dictionary<string, object>
                        {
                            {"body", respMap},
                            {"headers", response_.Headers},
                        });
                    }
                    if (!AlibabaCloud.TeaUtil.Common.Empty(response_.Headers.Get("x-ca-error-message")))
                    {
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                            {"message", response_.Headers.Get("x-ca-error-message")},
                        });
                    }
                    obj = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    respMap = AlibabaCloud.TeaUtil.Common.AssertAsMap(obj);
                    throw new TeaException(TeaConverter.merge<object>
                    (
                        new Dictionary<string, object>()
                        {
                            {"data", new Dictionary<string, object>
                            {
                                {"requestId", response_.Headers.Get("x-ca-request-id")},
                                {"statusCode", response_.StatusCode},
                                {"statusMessage", response_.StatusMessage},
                            }},
                        },
                        respMap
                    ));
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        /**
         * 取消绑定关系，生成新用户，返回访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public CancelLinkModel CancelLink(CancelLinkRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return CancelLinkEx(request, runtime);
        }

        /**
         * 取消绑定关系，生成新用户，返回访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<CancelLinkModel> CancelLinkAsync(CancelLinkRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await CancelLinkExAsync(request, runtime);
        }

        /**
         * 确认绑定关系, 成功后返回访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public ConfirmLinkModel ConfirmLink(ConfirmLinkRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return ConfirmLinkEx(request, runtime);
        }

        /**
         * 确认绑定关系, 成功后返回访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<ConfirmLinkModel> ConfirmLinkAsync(ConfirmLinkRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await ConfirmLinkExAsync(request, runtime);
        }

        /**
         * 修改手机登录密码，密码必须包含数字和字母，长度8-20个字符
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public ChangePasswordModel ChangePassword(DefaultChangePasswordRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return ChangePasswordEx(request, runtime);
        }

        /**
         * 修改手机登录密码，密码必须包含数字和字母，长度8-20个字符
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<ChangePasswordModel> ChangePasswordAsync(DefaultChangePasswordRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await ChangePasswordExAsync(request, runtime);
        }

        /**
         * 设置手机登录密码，密码必须包含数字和字母，长度8-20个字符
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public SetPasswordModel SetPassword(DefaultSetPasswordRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return SetPasswordEx(request, runtime);
        }

        /**
         * 设置手机登录密码，密码必须包含数字和字母，长度8-20个字符
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<SetPasswordModel> SetPasswordAsync(DefaultSetPasswordRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await SetPasswordExAsync(request, runtime);
        }

        /**
         * 校验手机短信验证码，用于重置密码时校验手机，通过校验后返回state，可通过state重新设置密码
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public VerifyCodeModel VerifyCode(VerifyCodeRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return VerifyCodeEx(request, runtime);
        }

        /**
         * 校验手机短信验证码，用于重置密码时校验手机，通过校验后返回state，可通过state重新设置密码
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<VerifyCodeModel> VerifyCodeAsync(VerifyCodeRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await VerifyCodeExAsync(request, runtime);
        }

        /**
         * 管理员通过账号信息直接获取用户的访问令牌
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public GetAccessTokenByLinkInfoModel GetAccessTokenByLinkInfo(GetAccessTokenByLinkInfoRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetAccessTokenByLinkInfoEx(request, runtime);
        }

        /**
         * 管理员通过账号信息直接获取用户的访问令牌
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<GetAccessTokenByLinkInfoModel> GetAccessTokenByLinkInfoAsync(GetAccessTokenByLinkInfoRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetAccessTokenByLinkInfoExAsync(request, runtime);
        }

        /**
         * 获取图片验证码，用于人机校验，默认不需要
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public GetCaptchaModel GetCaptcha(GetCaptchaRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetCaptchaEx(request, runtime);
        }

        /**
         * 获取图片验证码，用于人机校验，默认不需要
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<GetCaptchaModel> GetCaptchaAsync(GetCaptchaRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetCaptchaExAsync(request, runtime);
        }

        /**
         * 获取用户认证方式详情
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public GetLinkInfoModel GetLinkInfo(GetByLinkInfoRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetLinkInfoEx(request, runtime);
        }

        /**
         * 获取用户认证方式详情
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<GetLinkInfoModel> GetLinkInfoAsync(GetByLinkInfoRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetLinkInfoExAsync(request, runtime);
        }

        /**
         * 获取用户的所有绑定信息
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public GetLinkInfoByUserIdModel GetLinkInfoByUserId(GetLinkInfoByUserIDRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetLinkInfoByUserIdEx(request, runtime);
        }

        /**
         * 获取用户的所有绑定信息
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<GetLinkInfoByUserIdModel> GetLinkInfoByUserIdAsync(GetLinkInfoByUserIDRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetLinkInfoByUserIdExAsync(request, runtime);
        }

        /**
         * 获取公钥，用于加密对称密钥
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public GetPublicKeyModel GetPublicKey(GetAppPublicKeyRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetPublicKeyEx(request, runtime);
        }

        /**
         * 获取公钥，用于加密对称密钥
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<GetPublicKeyModel> GetPublicKeyAsync(GetAppPublicKeyRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetPublicKeyExAsync(request, runtime);
        }

        /**
         * 绑定用户认证方式
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         */
        public LinkModel Link(AccountLinkRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return LinkEx(request, runtime);
        }

        /**
         * 绑定用户认证方式
         * @tags account
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<LinkModel> LinkAsync(AccountLinkRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await LinkExAsync(request, runtime);
        }

        /**
         * 查询手机号是否已被注册
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public CheckExistModel CheckExist(MobileCheckExistRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return CheckExistEx(request, runtime);
        }

        /**
         * 查询手机号是否已被注册
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<CheckExistModel> CheckExistAsync(MobileCheckExistRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await CheckExistExAsync(request, runtime);
        }

        /**
         * 通过手机号+短信或密码登录，返回刷新令牌和访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public LoginModel Login(MobileLoginRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return LoginEx(request, runtime);
        }

        /**
         * 通过手机号+短信或密码登录，返回刷新令牌和访问令牌
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<LoginModel> LoginAsync(MobileLoginRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await LoginExAsync(request, runtime);
        }

        /**
         * 通过手机号+短信验证码注册账号
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         */
        public RegisterModel Register(MobileRegisterRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return RegisterEx(request, runtime);
        }

        /**
         * 通过手机号+短信验证码注册账号
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<RegisterModel> RegisterAsync(MobileRegisterRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await RegisterExAsync(request, runtime);
        }

        /**
         * 发送短信验证码，用于登录、注册、修改密码、绑定等
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public MobileSendSmsCodeModel MobileSendSmsCode(MobileSendSmsCodeRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return MobileSendSmsCodeEx(request, runtime);
        }

        /**
         * 发送短信验证码，用于登录、注册、修改密码、绑定等
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<MobileSendSmsCodeModel> MobileSendSmsCodeAsync(MobileSendSmsCodeRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await MobileSendSmsCodeExAsync(request, runtime);
        }

        /**
         * 用户退出登录
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public AccountRevokeModel AccountRevoke(RevokeRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return AccountRevokeEx(request, runtime);
        }

        /**
         * 用户退出登录
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<AccountRevokeModel> AccountRevokeAsync(RevokeRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await AccountRevokeExAsync(request, runtime);
        }

        /**
         * 用户通过刷新令牌（refresh_token）获取访问令牌（access_token）
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public AccountTokenModel AccountToken(TokenRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return AccountTokenEx(request, runtime);
        }

        /**
         * 用户通过刷新令牌（refresh_token）获取访问令牌（access_token）
         * @tags account
         * @error InvalidParameterMissing The input parameter {parameter_name} is missing.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<AccountTokenModel> AccountTokenAsync(TokenRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await AccountTokenExAsync(request, runtime);
        }

        /**
         * 列举Store列表
         * @tags admin
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public AdminListStoresModel AdminListStores(AdminListStoresRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return AdminListStoresEx(request, runtime);
        }

        /**
         * 列举Store列表
         * @tags admin
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error Forbidden User not authorized to operate on the specified APIs.
         * @error InternalError The request has been failed due to some unknown error.
         */
        public async Task<AdminListStoresModel> AdminListStoresAsync(AdminListStoresRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await AdminListStoresExAsync(request, runtime);
        }

        /**
         * 获取用户的accessToken
         * @tags admin
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         */
        public GetUserAccessTokenModel GetUserAccessToken(GetUserAccessTokenRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetUserAccessTokenEx(request, runtime);
        }

        /**
         * 获取用户的accessToken
         * @tags admin
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         * @error undefined undefined
         */
        public async Task<GetUserAccessTokenModel> GetUserAccessTokenAsync(GetUserAccessTokenRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetUserAccessTokenExAsync(request, runtime);
        }

        /**
         * 支持normal和large两种drive，
         * large类型的drive用于文件数多的场景，不支持list操作，
         * 当drive的文件数量大于1亿时，建议使用large类型。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CreateDriveModel CreateDrive(CreateDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return CreateDriveEx(request, runtime);
        }

        /**
         * 支持normal和large两种drive，
         * large类型的drive用于文件数多的场景，不支持list操作，
         * 当drive的文件数量大于1亿时，建议使用large类型。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CreateDriveModel> CreateDriveAsync(CreateDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await CreateDriveExAsync(request, runtime);
        }

        /**
         * 删除指定drive_id对应的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public DeleteDriveModel DeleteDrive(DeleteDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return DeleteDriveEx(request, runtime);
        }

        /**
         * 删除指定drive_id对应的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<DeleteDriveModel> DeleteDriveAsync(DeleteDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await DeleteDriveExAsync(request, runtime);
        }

        /**
         * 获取指定drive_id对应的Drive详细信息。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetDriveModel GetDrive(GetDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetDriveEx(request, runtime);
        }

        /**
         * 获取指定drive_id对应的Drive详细信息。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetDriveModel> GetDriveAsync(GetDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetDriveExAsync(request, runtime);
        }

        /**
         * 一个用户可拥有多个drive，在创建drive时通过参数指定是否为这个用户的默认drive，
         * 每个用户只能设置一个默认drive。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetDefaultDriveModel GetDefaultDrive(GetDefaultDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetDefaultDriveEx(request, runtime);
        }

        /**
         * 一个用户可拥有多个drive，在创建drive时通过参数指定是否为这个用户的默认drive，
         * 每个用户只能设置一个默认drive。
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetDefaultDriveModel> GetDefaultDriveAsync(GetDefaultDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetDefaultDriveExAsync(request, runtime);
        }

        /**
         * 管理员列举指定用户的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListDrivesModel ListDrives(ListDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return ListDrivesEx(request, runtime);
        }

        /**
         * 管理员列举指定用户的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListDrivesModel> ListDrivesAsync(ListDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await ListDrivesExAsync(request, runtime);
        }

        /**
         * 列举当前用户（访问令牌）的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListMyDrivesModel ListMyDrives(ListMyDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return ListMyDrivesEx(request, runtime);
        }

        /**
         * 列举当前用户（访问令牌）的Drive
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListMyDrivesModel> ListMyDrivesAsync(ListMyDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await ListMyDrivesExAsync(request, runtime);
        }

        /**
         * 更新指定drive_id的Drive信息
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public UpdateDriveModel UpdateDrive(UpdateDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return UpdateDriveEx(request, runtime);
        }

        /**
         * 更新指定drive_id的Drive信息
         * @tags drive
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<UpdateDriveModel> UpdateDriveAsync(UpdateDriveRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await UpdateDriveExAsync(request, runtime);
        }

        /**
         * 完成文件上传
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CompleteFileModel CompleteFile(HostingCompleteFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return CompleteFileEx(request, runtime);
        }

        /**
         * 完成文件上传
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CompleteFileModel> CompleteFileAsync(HostingCompleteFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await CompleteFileExAsync(request, runtime);
        }

        /**
         * 指定源文件或文件夹路径，拷贝到指定的文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CopyFileModel CopyFile(HostingCopyFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return CopyFileEx(request, runtime);
        }

        /**
         * 指定源文件或文件夹路径，拷贝到指定的文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CopyFileModel> CopyFileAsync(HostingCopyFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await CopyFileExAsync(request, runtime);
        }

        /**
         * 创建文件或者文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CreateFileModel CreateFile(HostingCreateFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return CreateFileEx(request, runtime);
        }

        /**
         * 创建文件或者文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error AlreadyExist {resource} has already exists. {extra_msg}
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CreateFileModel> CreateFileAsync(HostingCreateFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await CreateFileExAsync(request, runtime);
        }

        /**
         * 指定文件或文件夹路径，删除文件或文件夹
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public DeleteFileModel DeleteFile(HostingDeleteFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return DeleteFileEx(request, runtime);
        }

        /**
         * 指定文件或文件夹路径，删除文件或文件夹
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<DeleteFileModel> DeleteFileAsync(HostingDeleteFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await DeleteFileExAsync(request, runtime);
        }

        /**
         * 获取指定文件或文件夹路径，获取文件或文件夹信息。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetFileModel GetFile(HostingGetFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetFileEx(request, runtime);
        }

        /**
         * 获取指定文件或文件夹路径，获取文件或文件夹信息。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetFileModel> GetFileAsync(HostingGetFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetFileExAsync(request, runtime);
        }

        /**
         * 指定文件路径，获取文件下载地址
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetDownloadUrlModel GetDownloadUrl(HostingGetDownloadUrlRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetDownloadUrlEx(request, runtime);
        }

        /**
         * 指定文件路径，获取文件下载地址
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetDownloadUrlModel> GetDownloadUrlAsync(HostingGetDownloadUrlRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetDownloadUrlExAsync(request, runtime);
        }

        /**
         * 指定文件路径，获取文件安全地址
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetSecureUrlModel GetSecureUrl(HostingGetSecureUrlRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetSecureUrlEx(request, runtime);
        }

        /**
         * 指定文件路径，获取文件安全地址
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetSecureUrlModel> GetSecureUrlAsync(HostingGetSecureUrlRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetSecureUrlExAsync(request, runtime);
        }

        /**
         * 可指定分片信息，一次获取多个分片的上传地址。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetUploadUrlModel GetUploadUrl(HostingGetUploadUrlRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetUploadUrlEx(request, runtime);
        }

        /**
         * 可指定分片信息，一次获取多个分片的上传地址。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetUploadUrlModel> GetUploadUrlAsync(HostingGetUploadUrlRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetUploadUrlExAsync(request, runtime);
        }

        /**
         * 指定父文件夹路径，列举文件夹下的文件或者文件夹
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListFileModel ListFile(HostingListFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return ListFileEx(request, runtime);
        }

        /**
         * 指定父文件夹路径，列举文件夹下的文件或者文件夹
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListFileModel> ListFileAsync(HostingListFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await ListFileExAsync(request, runtime);
        }

        /**
         * 列举UploadID对应的已上传分片。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListUploadedPartsModel ListUploadedParts(HostingListUploadedPartRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return ListUploadedPartsEx(request, runtime);
        }

        /**
         * 列举UploadID对应的已上传分片。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListUploadedPartsModel> ListUploadedPartsAsync(HostingListUploadedPartRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await ListUploadedPartsExAsync(request, runtime);
        }

        /**
         * 指定源文件或文件夹路径，移动到指定的文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public MoveFileModel MoveFile(HostingMoveFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return MoveFileEx(request, runtime);
        }

        /**
         * 指定源文件或文件夹路径，移动到指定的文件夹。
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<MoveFileModel> MoveFileAsync(HostingMoveFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await MoveFileExAsync(request, runtime);
        }

        /**
         * 获取视频支持的分辨率
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public VideoDefinitionModel VideoDefinition(HostingVideoDefinitionRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return VideoDefinitionEx(request, runtime);
        }

        /**
         * 获取视频支持的分辨率
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<VideoDefinitionModel> VideoDefinitionAsync(HostingVideoDefinitionRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await VideoDefinitionExAsync(request, runtime);
        }

        /**
         * 获取视频的DRM License
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public VideoLicenseModel VideoLicense(HostingVideoDRMLicenseRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return VideoLicenseEx(request, runtime);
        }

        /**
         * 获取视频的DRM License
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<VideoLicenseModel> VideoLicenseAsync(HostingVideoDRMLicenseRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await VideoLicenseExAsync(request, runtime);
        }

        /**
         * 获取视频转码后的m3u8文件
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public VideoM3u8Model VideoM3u8(HostingVideoM3U8Request request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return VideoM3u8Ex(request, runtime);
        }

        /**
         * 获取视频转码后的m3u8文件
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<VideoM3u8Model> VideoM3u8Async(HostingVideoM3U8Request request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await VideoM3u8ExAsync(request, runtime);
        }

        /**
         * 将mp4格式的视频文件，转为m3u8
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public VideoTranscodeModel VideoTranscode(HostingVideoTranscodeRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return VideoTranscodeEx(request, runtime);
        }

        /**
         * 将mp4格式的视频文件，转为m3u8
         * @tags file
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<VideoTranscodeModel> VideoTranscodeAsync(HostingVideoTranscodeRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await VideoTranscodeExAsync(request, runtime);
        }

        /**
         * 创建共享。
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CreateShareModel CreateShare(CreateShareRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return CreateShareEx(request, runtime);
        }

        /**
         * 创建共享。
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CreateShareModel> CreateShareAsync(CreateShareRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await CreateShareExAsync(request, runtime);
        }

        /**
         * 删除指定共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public DeleteShareModel DeleteShare(DeleteShareRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return DeleteShareEx(request, runtime);
        }

        /**
         * 删除指定共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<DeleteShareModel> DeleteShareAsync(DeleteShareRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await DeleteShareExAsync(request, runtime);
        }

        /**
         * 获取共享信息。
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetShareModel GetShare(GetShareRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetShareEx(request, runtime);
        }

        /**
         * 获取共享信息。
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetShareModel> GetShareAsync(GetShareRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetShareExAsync(request, runtime);
        }

        /**
         * 列举指定用户的共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListShareModel ListShare(ListShareRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return ListShareEx(request, runtime);
        }

        /**
         * 列举指定用户的共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListShareModel> ListShareAsync(ListShareRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await ListShareExAsync(request, runtime);
        }

        /**
         * 修改指定共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public UpdateShareModel UpdateShare(UpdateShareRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return UpdateShareEx(request, runtime);
        }

        /**
         * 修改指定共享
         * @tags share
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<UpdateShareModel> UpdateShareAsync(UpdateShareRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await UpdateShareExAsync(request, runtime);
        }

        /**
         * 列举指定store下的所有文件。
         * @tags store
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListStorefileModel ListStorefile(ListStoreFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return ListStorefileEx(request, runtime);
        }

        /**
         * 列举指定store下的所有文件。
         * @tags store
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListStorefileModel> ListStorefileAsync(ListStoreFileRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await ListStorefileExAsync(request, runtime);
        }

        /**
         * 创建用户，只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public CreateUserModel CreateUser(CreateUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return CreateUserEx(request, runtime);
        }

        /**
         * 创建用户，只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<CreateUserModel> CreateUserAsync(CreateUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await CreateUserExAsync(request, runtime);
        }

        /**
         * 只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public DeleteUserModel DeleteUser(DeleteUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return DeleteUserEx(request, runtime);
        }

        /**
         * 只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<DeleteUserModel> DeleteUserAsync(DeleteUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await DeleteUserExAsync(request, runtime);
        }

        /**
         * 获取用户详细信息，普通用户只能获取自己的信息，管理员可以获取任意用户的信息。
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public GetUserModel GetUser(GetUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return GetUserEx(request, runtime);
        }

        /**
         * 获取用户详细信息，普通用户只能获取自己的信息，管理员可以获取任意用户的信息。
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<GetUserModel> GetUserAsync(GetUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await GetUserExAsync(request, runtime);
        }

        /**
         * 只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public ListUsersModel ListUsers(ListUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return ListUsersEx(request, runtime);
        }

        /**
         * 只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<ListUsersModel> ListUsersAsync(ListUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await ListUsersExAsync(request, runtime);
        }

        /**
         * 该接口将会根据条件查询用户，只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public SearchUserModel SearchUser(SearchUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return SearchUserEx(request, runtime);
        }

        /**
         * 该接口将会根据条件查询用户，只有管理员可以调用
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<SearchUserModel> SearchUserAsync(SearchUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await SearchUserExAsync(request, runtime);
        }

        /**
         * 用户可以修改自己的description，nick_name，avatar；
         * 管理员在用户基础上还可修改status（可以修改任意用户）；
         * 超级管理员在管理员基础上还可修改role（可以修改任意用户）。
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public UpdateUserModel UpdateUser(UpdateUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return UpdateUserEx(request, runtime);
        }

        /**
         * 用户可以修改自己的description，nick_name，avatar；
         * 管理员在用户基础上还可修改status（可以修改任意用户）；
         * 超级管理员在管理员基础上还可修改role（可以修改任意用户）。
         * @tags user
         * @error InvalidParameter The input parameter {parameter_name} is not valid.
         * @error AccessTokenInvalid AccessToken is invalid. {message}
         * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
         * @error NotFound The resource {resource_name} cannot be found. Please check.
         * @error InternalError The request has been failed due to some unknown error.
         * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
         */
        public async Task<UpdateUserModel> UpdateUserAsync(UpdateUserRequest request)
        {
            RuntimeOptions runtime = new RuntimeOptions();
            return await UpdateUserExAsync(request, runtime);
        }

        public string GetPathname(string nickname, string path)
        {
            if (AlibabaCloud.TeaUtil.Common.Empty(nickname))
            {
                return path;
            }
            return "/" + nickname + path;
        }

        public void SetExpireTime(string expireTime)
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_accessTokenCredential))
            {
                return ;
            }
            this._accessTokenCredential.SetExpireTime(expireTime);
        }

        public async Task SetExpireTimeAsync(string expireTime)
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_accessTokenCredential))
            {
                return ;
            }
            this._accessTokenCredential.SetExpireTime(expireTime);
        }

        public string GetExpireTime()
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_accessTokenCredential))
            {
                return "";
            }
            string expireTime = this._accessTokenCredential.GetExpireTime();
            return expireTime;
        }

        public void SetRefreshToken(string token)
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_accessTokenCredential))
            {
                return ;
            }
            this._accessTokenCredential.SetRefreshToken(token);
        }

        public string GetRefreshToken()
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_accessTokenCredential))
            {
                return "";
            }
            string token = this._accessTokenCredential.GetRefreshToken();
            return token;
        }

        public void SetAccessToken(string token)
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_accessTokenCredential))
            {
                return ;
            }
            this._accessTokenCredential.SetAccessToken(token);
        }

        public string GetAccessToken()
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_accessTokenCredential))
            {
                return "";
            }
            string token = this._accessTokenCredential.GetAccessToken();
            return token;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_accessTokenCredential))
            {
                return "";
            }
            string token = await this._accessTokenCredential.GetAccessTokenAsync();
            return token;
        }

        public void SetUserAgent(string userAgent)
        {
            this._userAgent = userAgent;
        }

        public void AppendUserAgent(string userAgent)
        {
            this._userAgent = _userAgent + " " + userAgent;
        }

        public string GetUserAgent()
        {
            string userAgent = AlibabaCloud.TeaUtil.Common.GetUserAgent(_userAgent);
            return userAgent;
        }

        public string GetAccessKeyId()
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_credential))
            {
                return "";
            }
            string accessKeyId = this._credential.GetAccessKeyId();
            return accessKeyId;
        }

        public async Task<string> GetAccessKeyIdAsync()
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_credential))
            {
                return "";
            }
            string accessKeyId = await this._credential.GetAccessKeyIdAsync();
            return accessKeyId;
        }

        public string GetAccessKeySecret()
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_credential))
            {
                return "";
            }
            string secret = this._credential.GetAccessKeySecret();
            return secret;
        }

        public async Task<string> GetAccessKeySecretAsync()
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_credential))
            {
                return "";
            }
            string secret = await this._credential.GetAccessKeySecretAsync();
            return secret;
        }

        public string GetSecurityToken()
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_credential))
            {
                return "";
            }
            string token = this._credential.GetSecurityToken();
            return token;
        }

        public async Task<string> GetSecurityTokenAsync()
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(_credential))
            {
                return "";
            }
            string token = await this._credential.GetSecurityTokenAsync();
            return token;
        }

    }
}
