<?php

// This file is auto-generated, don't edit it. Thanks.

namespace Aliyun\PDS\SDK\Models;

use AlibabaCloud\Tea\Model;

/**
 * Update face group info request.
 */
class UpdateFaceGroupInfoRequest extends Model
{
    public $headers;

    /**
     * @description drive_id
     *
     * @example "101"
     *
     * @var string
     */
    public $driveId;

    /**
     * @description group_id 列举人脸分组接口中获取
     *
     * @example "group-asdasdasdeop"
     *
     * @var string
     */
    public $groupId;

    /**
     * @description group_name
     *
     * @example "张XX"
     *
     * @var string
     */
    public $groupName;
    protected $_name = [
        'driveId'   => 'drive_id',
        'groupId'   => 'group_id',
        'groupName' => 'group_name',
    ];

    public function validate()
    {
        Model::validateRequired('driveId', $this->driveId, true);
        Model::validateRequired('groupId', $this->groupId, true);
        Model::validatePattern('driveId', $this->driveId, '[0-9]+');
    }

    public function toMap()
    {
        $res = [];
        if (null !== $this->headers) {
            $res['headers'] = $this->headers;
        }
        if (null !== $this->driveId) {
            $res['drive_id'] = $this->driveId;
        }
        if (null !== $this->groupId) {
            $res['group_id'] = $this->groupId;
        }
        if (null !== $this->groupName) {
            $res['group_name'] = $this->groupName;
        }

        return $res;
    }

    /**
     * @param array $map
     *
     * @return UpdateFaceGroupInfoRequest
     */
    public static function fromMap($map = [])
    {
        $model = new self();
        if (isset($map['headers'])) {
            $model->headers = $map['headers'];
        }
        if (isset($map['drive_id'])) {
            $model->driveId = $map['drive_id'];
        }
        if (isset($map['group_id'])) {
            $model->groupId = $map['group_id'];
        }
        if (isset($map['group_name'])) {
            $model->groupName = $map['group_name'];
        }

        return $model;
    }
}
