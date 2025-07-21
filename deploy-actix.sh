#!/bin/bash
set -e

APP_NAME="node-id-api-rust"
IMAGE_NAME="$APP_NAME:latest"
DOCKERFILE_PATH="node_id_api/Dockerfile"  # Assuming it's at the project root
K8S_FILE="node-id-api-rust.yaml"
TAR_NAME="$APP_NAME.tar"

echo "🛠️  Building image with Docker..."
docker build -t $IMAGE_NAME -f $DOCKERFILE_PATH .

echo "📦 Saving image as tarball..."
rm -f $TAR_NAME
docker save -o $TAR_NAME $IMAGE_NAME

echo "📥 Loading image tarball into Minikube..."
minikube image load $TAR_NAME

echo "🚀 Applying Kubernetes config..."
kubectl apply -f $K8S_FILE

echo "⏳ Waiting for deployment to be ready..."
kubectl rollout status deployment/$APP_NAME
