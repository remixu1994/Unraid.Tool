# qbittorrent API 文档

这是一个与qbittorrent交互的RESTful API，使用C#后端。该API提供了登录、获取种子、添加、编辑或删除跟踪器等功能。

## 基础URL


## 接口

- **登录**
    - **方法:** `POST`
    - **接口:** `/api/qbittorrent/Login`
    - **描述:** 使用提供的凭据登录qbittorrent实例。
    - **请求体:**
      ```json
      {
        "Host": "http://localhost:8080",
        "UserName": "your_username",
        "Password": "your_password"
      }
      ```
    - **响应:**
      ```json
      {
        "message": "Login successfully."
      }
      ```

- **获取所有种子**
    - **方法:** `GET`
    - **接口:** `/api/qbittorrent/torrents`
    - **描述:** 获取qbittorrent实例中的所有种子。

- **添加跟踪器**
    - **方法:** `POST`
    - **接口:** `/api/qbittorrent/add-trackers/{torrentHash}`
    - **描述:** 向指定的种子添加跟踪器。
    - **请求体:**
      ```json
      "tracker_url"
      ```

- **编辑跟踪器**
    - **方法:** `POST`
    - **接口:** `/api/qbittorrent/edit-tracker/{torrentHash}`
    - **描述:** 编辑指定种子中已存在跟踪器的URL。
    - **请求体:**
      ```json
      {
        "OrigUrl": "original_tracker_url",
        "NewUrl": "new_tracker_url"
      }
      ```

- **替换跟踪器**
    - **方法:** `POST`
    - **接口:** `/api/qbittorrent/replace-tracker`
    - **描述:** 替换所有种子中的原始跟踪器URL为新的URL。
    - **请求体:**
      ```json
      {
        "OrigUrl": "original_tracker_url",
        "NewUrl": "new_tracker_url"
      }
      ```

- **删除跟踪器**
    - **方法:** `DELETE`
    - **接口:** `/api/qbittorrent/delete-tracker/{torrentHash}`
    - **描述:** 从指定种子中删除跟踪器。
    - **查询参数:**
        - `trackerUrl`: 要删除的跟踪器URL。

## 错误处理

- `200 OK`: 请求成功。
- `400 Bad Request`: 用户未认证或请求格式不正确。
- `404 Not Found`: 请求的资源未找到（例如无效的种子哈希值）。

## 注意事项

- 需要先通过`Login`接口进行认证，才能访问其他接口。
- 在发送请求时，请确保跟踪器URL已正确编码。
