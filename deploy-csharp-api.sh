#!/bin/bash
set -e

APP_NAME="node-id-api-csharp"
IMAGE_NAME="$APP_NAME:latest"
DOCKERFILE_PATH="NodeId.Api/Dockerfile"
K8S_FILE="node-id-api-csharp.yaml"
TAR_NAME="$APP_NAME.tar"

echo "🛠️ Building image with Docker..."
docker build -t $IMAGE_NAME -f $DOCKERFILE_PATH .

echo "📦 Saving image as tarball..."
rm -f $TAR_NAME
docker save -o $TAR_NAME $IMAGE_NAME

echo "📥 Loading image tarball into Minikube..."
minikube image load $TAR_NAME

echo "Applying Kubernetes config..."
kubectl apply -f $K8S_FILE

echo "⏳ Waiting for deployment to be ready..."
kubectl rollout status deployment/$APP_NAME
