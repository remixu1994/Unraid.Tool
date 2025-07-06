#!/bin/bash

# 检查是否安装了 ffmpeg
if ! command -v ffmpeg &> /dev/null; then
    echo "❌ 错误：ffmpeg 未安装，请先安装 ffmpeg！"
    exit 1
fi

# 遍历当前目录下的所有 .ass 文件
for file in *.ass; do
    if [[ -f "$file" ]]; then
        # 获取文件名（不含扩展名）
        filename="${file%.*}"
        
        # 使用 ffmpeg 转换
        echo "🔧 正在转换: $file → $filename.srt"
        ffmpeg -i "$file" "$filename.srt" -loglevel warning -y
        
        # 检查是否成功
        if [[ $? -eq 0 ]]; then
            echo "✅ 转换成功: $filename.srt"
        else
            echo "❌ 转换失败: $file"
        fi
    fi
done

echo "🎉 所有转换完成！"