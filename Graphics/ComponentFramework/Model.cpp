#include "Model.h"
#include "Shader.h"
#include "ObjLoader.h"
#include "Camera.h"
#include "SceneEnvironment.h"
#include "Trackball.h"
#include <SDL.h>
#include "SDL_image.h"

namespace GAME {

	Model::Model() {



	}

	Model::Model(const Vec3 pos_, const Vec3 orientation_) {
		pos = pos_;
		orientation = orientation_;
		shader = nullptr;
	}

	Model::~Model() {
		OnDestroy();
	}

	Matrix4 Model::getmodelMatrix() {
		return modelMatrix;
	}

	void Model::setmodelMatrix(Matrix4 m) {
		modelMatrix = m;
	}

	void Model::setPos(const Vec3& pos_) {
		Entity::setPos(pos_);
		updateModelMatrix();
	}

	void Model::setOrientation(const Vec3& orientation_) {
		Entity::setOrientation(orientation_);
		updateModelMatrix();
	}

	void Model::updateModelMatrix() {
		modelMatrix = MMath::translate(pos);

		/// This transform is based on Euler angles - let's do it later
		///modelMatrix = MMath::translate(pos) * MMath::rotate(orientation.z, Vec3(0.0f, 0.0f, 1.0f)) * MMath::rotate(orientation.x, Vec3(1.0f, 0.0f, 0.0f)) * MMath::rotate(orientation.y, Vec3(0.0f, 1.0f, 0.0f));
	}

	bool Model::OnCreate() {
		// **TOON SHADER**
		//shader = new Shader("toonVertex.bartholemew", "toonFrag.bartholemew", 2, 0, "vVertex", 1, "vNormal");
		// **REFLECTION SHADER**
	    //shader = new Shader("reflectionVert.glsl", "reflectionFrag.glsl", 2, 0, "vVertex", 1, "vNormal");
		// **FIREBALL SHADER**
		shader = new Shader("fireballVert.glsl", "fireballFrag.glsl", 2, 0, "vVertex", 1, "vNormal");

		//reflectionShader = new Shader("reflectionVert.glsl", "reflectionFrag.glsl", 1, 0, "vVertex");
		glGenTextures(1, textureID);

		glBindTexture(GL_TEXTURE_2D, textureID[0]);

		///Load the image from the hard drive 
		SDL_Surface *textureSurface = IMG_Load("skull_texture.jpg");
		if (textureSurface == nullptr) {
			return false;
		}
		/// Are we using alpha? Not in jpeg but let's be careful
		int mode = (textureSurface->format->BytesPerPixel == 4) ? GL_RGBA : GL_RGB;

		/// Wrapping and filtering options
		glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
		/// Load the texture data from the SDL_Surface to the GPU memmory
		glTexImage2D(GL_TEXTURE_2D, 0, mode, textureSurface->w, textureSurface->h, 0, mode, GL_UNSIGNED_BYTE, textureSurface->pixels);
		/// Release the memory
		SDL_FreeSurface(textureSurface); /// let go of the memory
		return true;
	}



	bool Model::LoadMesh(const char* filename) {
		if (ObjLoader::loadOBJ(filename) == false) {
			return false;
		}
		/// Get the data out of the ObjLoader and into our own mesh
		meshes.push_back(new Mesh(GL_TRIANGLES, ObjLoader::vertices, ObjLoader::normals, ObjLoader::uvCoords));
		return true;
	}


	void Model::Update(const float deltaTime) {
		/// See Entity.h
		///Rotate(Vec3(0.0f, 50.0f * deltaTime, 0.0f));
	}

	void Model::Render() const {

		GLint projectionMatrixID = glGetUniformLocation(shader->getProgram(), "projectionMatrix");
		GLint viewMatrixID = glGetUniformLocation(shader->getProgram(), "viewMatrix");
		GLint modelMatrixID = glGetUniformLocation(shader->getProgram(), "modelMatrix");
		GLint normalMatrixID = glGetUniformLocation(shader->getProgram(), "normalMatrix");
		GLint lightPosID = glGetUniformLocation(shader->getProgram(), "lightPos");

		glUseProgram(shader->getProgram());

		glUniformMatrix4fv(projectionMatrixID, 1, GL_FALSE, Camera::currentCamera->getProjectionMatrix());
		glUniformMatrix4fv(viewMatrixID, 1, GL_FALSE, Camera::currentCamera->getViewMatrix());
		glUniformMatrix4fv(modelMatrixID, 1, GL_FALSE, modelMatrix * Trackball::getInstance()->getMatrix4());

		/// Assigning the 4x4 modelMatrix to the 3x3 normalMatrix 
		/// copies just the upper 3x3 of the modelMatrix
		Matrix3 normalMatrix = modelMatrix * Trackball::getInstance()->getMatrix4();
		glUniformMatrix3fv(normalMatrixID, 1, GL_FALSE, normalMatrix);

		glUniform3fv(lightPosID, 1, SceneEnvironment::getInstance()->getLight());

		for (Mesh* mesh : meshes) {
			mesh->Render();
		}


	}
	void Model::OnDestroy() {
		if (shader) delete shader;
	}
}