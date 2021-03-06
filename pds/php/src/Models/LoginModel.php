<?php

// This file is auto-generated, don't edit it. Thanks.

namespace Aliyun\PDS\SDK\Models;

use AlibabaCloud\Tea\Model;

class LoginModel extends Model
{
    /**
     * @var array
     */
    public $headers;

    /**
     * @var AccountAccessTokenResponse
     */
    public $body;
    protected $_name = [
        'headers' => 'headers',
        'body'    => 'body',
    ];

    public function validate()
    {
        Model::validateRequired('body', $this->body, true);
    }

    public function toMap()
    {
        $res = [];
        if (null !== $this->headers) {
            $res['headers'] = $this->headers;
        }
        if (null !== $this->body) {
            $res['body'] = null !== $this->body ? $this->body->toMap() : null;
        }

        return $res;
    }

    /**
     * @param array $map
     *
     * @return LoginModel
     */
    public static function fromMap($map = [])
    {
        $model = new self();
        if (isset($map['headers'])) {
            $model->headers = $map['headers'];
        }
        if (isset($map['body'])) {
            $model->body = AccountAccessTokenResponse::fromMap($map['body']);
        }

        return $model;
    }
}
