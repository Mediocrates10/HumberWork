#include "SkyBox.h"
#include "Shader.h"
#include "ObjLoader.h"
#include "Camera.h"
#include "SceneEnvironment.h"
#include "Trackball.h"
#include <GL/glew.h>
#include <SDL.h>
#include <SDL_image.h>


namespace GAME {

	SkyBox::SkyBox(const char * positive_x_image_,
		const char * negative_x_image_,
		const char * positive_y_image_,
		const char * negative_y_image_,
		const char * positive_z_image_,
		const char * negative_z_image_) {

		positive_x_image = positive_x_image_;
		negative_x_image = negative_x_image_;
		positive_y_image = positive_y_image_;
		negative_y_image = negative_y_image_;
		positive_z_image = positive_z_image_;
		negative_z_image = negative_z_image_;

	}

	SkyBox::~SkyBox() {



	}

	bool SkyBox::OnCreate() {

		ObjLoader obj;

		if (obj.loadOBJ("sphere.obj") == false) {
			return false;
		}

		meshes.push_back(new Mesh(GL_TRIANGLES, ObjLoader::vertices, ObjLoader::normals, ObjLoader::uvCoords));

		SDL_Surface *textureSurface;

		glGenTextures(1, &texture);
		glBindTexture(GL_TEXTURE_CUBE_MAP, texture);
		/// Wrapping and filtering options
		glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
		glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
		glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_R, GL_CLAMP_TO_EDGE);

		textureSurface = IMG_Load(positive_x_image);
		if (textureSurface == nullptr) return false;
		glTexImage2D(GL_TEXTURE_CUBE_MAP_POSITIVE_X, 0, GL_RGB, textureSurface->w, textureSurface->h,
			0, GL_RGB, GL_UNSIGNED_BYTE, textureSurface->pixels);
		SDL_FreeSurface(textureSurface);
		textureSurface = nullptr;

		textureSurface = IMG_Load(negative_x_image);
		if (textureSurface == nullptr) return false;
		///mode = (textureSurface->format->BytesPerPixel == 4) ? GL_RGBA : GL_RGB;
		glTexImage2D(GL_TEXTURE_CUBE_MAP_NEGATIVE_X, 0, GL_RGB, textureSurface->w, textureSurface->h,
			0, GL_RGB, GL_UNSIGNED_BYTE, textureSurface->pixels);
		SDL_FreeSurface(textureSurface);
		textureSurface = nullptr;


		textureSurface = IMG_Load(positive_y_image);
		if (textureSurface == nullptr) return false;
		///mode = (textureSurface->format->BytesPerPixel == 4) ? GL_RGBA : GL_RGB;
		glTexImage2D(GL_TEXTURE_CUBE_MAP_POSITIVE_Y, 0, GL_RGB, textureSurface->w, textureSurface->h,
			0, GL_RGB, GL_UNSIGNED_BYTE, textureSurface->pixels);
		SDL_FreeSurface(textureSurface);
		textureSurface = nullptr;

		textureSurface = IMG_Load(negative_y_image);
		if (textureSurface == nullptr) return false;
		///mode = (textureSurface->format->BytesPerPixel == 4) ? GL_RGBA : GL_RGB;
		glTexImage2D(GL_TEXTURE_CUBE_MAP_NEGATIVE_Y, 0, GL_RGB, textureSurface->w, textureSurface->h,
			0, GL_RGB, GL_UNSIGNED_BYTE, textureSurface->pixels);
		SDL_FreeSurface(textureSurface);
		textureSurface = nullptr;


		textureSurface = IMG_Load(positive_z_image);
		if (textureSurface == nullptr) return false;
		///mode = (textureSurface->format->BytesPerPixel == 4) ? GL_RGBA : GL_RGB;
		glTexImage2D(GL_TEXTURE_CUBE_MAP_POSITIVE_Z, 0, GL_RGB, textureSurface->w, textureSurface->h,
			0, GL_RGB, GL_UNSIGNED_BYTE, textureSurface->pixels);
		SDL_FreeSurface(textureSurface);
		textureSurface = nullptr;


		textureSurface = IMG_Load(negative_z_image);
		if (textureSurface == nullptr) return false;
		///mode = (textureSurface->format->BytesPerPixel == 4) ? GL_RGBA : GL_RGB;
		glTexImage2D(GL_TEXTURE_CUBE_MAP_NEGATIVE_Z, 0, GL_RGB, textureSurface->w, textureSurface->h,
			0, GL_RGB, GL_UNSIGNED_BYTE, textureSurface->pixels);
		SDL_FreeSurface(textureSurface);
		textureSurface = nullptr;



		SkyBoxShader = new Shader("skyboxVert.glsl", "skyboxFrag.glsl", 1, 0, "vVertex");
		//reflectionShader = new Shader("reflectionVert.glsl", "reflectionFrag.glsl", 1, 0, "vVertex");
		projectionMatrixID = glGetUniformLocation(SkyBoxShader->getProgram(), "projectionMatrix");
		/// You may prefer to send the view and model matrices separately 
		modelViewMatrixID = glGetUniformLocation(SkyBoxShader->getProgram(), "modelViewMatrix");
		textureID = glGetUniformLocation(SkyBoxShader->getProgram(), "cubeTexture");
		return true;


	}

	void SkyBox::Render(const Matrix4& projectionMatrix, const Matrix4& modelViewMatrix) const {
		glDisable(GL_DEPTH_TEST);
		glDisable(GL_CULL_FACE);
		glUseProgram(SkyBoxShader->getProgram());
		GLint normalMatrixID = glGetUniformLocation(SkyBoxShader->getProgram(), "modelMatrix");
		glBindTexture(GL_TEXTURE_CUBE_MAP, texture);
		/*glUniformMatrix4fv(projectionMatrixID, 1, GL_FALSE, projectionMatrix);
		glUniformMatrix4fv(modelViewMatrixID, 1, GL_FALSE, modelViewMatrix);*/
		glUniformMatrix4fv(projectionMatrixID, 1, GL_FALSE, Camera::currentCamera->getProjectionMatrix());
		glUniformMatrix4fv(modelViewMatrixID, 1, GL_FALSE, modelViewMatrix * Trackball::getInstance()->getMatrix4());
		/*Matrix3 normalMatrix = modelMatrix * Trackball::getInstance()->getMatrix4();
		glUniformMatrix3fv(normalMatrixID, 1, GL_FALSE, modelMatrix);*/

		for (Mesh* mesh : meshes) {

			mesh->Render();

		}


		glEnableVertexAttribArray(0);
		glEnable(GL_DEPTH_TEST);
		glEnable(GL_CULL_FACE);

	}

	void SkyBox::Update(const float time) {



	}

	void SkyBox::OnDestroy() {

		if (SkyBoxShader) delete SkyBoxShader;

	}
}