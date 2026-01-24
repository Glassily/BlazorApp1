# 项目快速入门指南
欢迎使用 **AwesomeApp**！本文档将帮助你快速上手。

## ✨ 特性

- 🚀 高性能渲染
- 🔒 内置安全防护
- 🌐 多语言支持
- 🎨 主题自定义

## 🛠 安装

使用 .NET CLI 安装：

```bash
dotnet add package AwesomeApp.Core

```
---

### 📄 示例 2：博客文章风格（带引用和图片占位）


# 如何写出优雅的 Markdown？

Markdown 不仅是写作工具，更是一种表达艺术。

## 引言

> “简洁是智慧的灵魂。” —— 莎士比亚

在信息爆炸的时代，清晰、结构化的文档比以往更重要。

## 为什么选择 Markdown？

1. **纯文本**：兼容所有系统
2. **易读易写**：无需鼠标操作
3. **广泛支持**：GitHub、Notion、Obsidian 等均原生支持

## 代码高亮示例

Python 实现斐波那契数列：

```python
def fib(n):
    a, b = 0, 1
    for _ in range(n):
        yield a
        a, b = b, a + b

print(list(fib(10)))
```


---


### 📄 示例 3：API 接口文档风格（含复杂表格）

# 用户管理 API 文档

## 获取用户信息

**Endpoint**: `GET /api/users/{id}`

### 请求参数

| 参数 | 位置 | 必填 | 类型 | 说明 |
|------|------|------|------|------|
| `id` | path | 是 | integer | 用户 ID |
| `includeProfile` | query | 否 | boolean | 是否包含完整资料 |

### 响应示例

```json
{
  "id": 123,
  "username": "alice",
  "email": "alice@example.com",
  "profile": {
    "avatar": "https://example.com/avatar.jpg",
    "bio": "Software engineer"
  }
}
```
