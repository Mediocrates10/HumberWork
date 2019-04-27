#include <GL/glew.h>
#include <SDL.h>
#include <iostream>
#include "MMath.h"
#include "Window.h"
#include "Shader.h"
#include "Scene1.h"
#include "SceneEnvironment.h"
#include "Trackball.h"
#include "ObjLoader.h"

using namespace GAME;
using namespace MATH;


Scene1::Scene1(Window& windowRef) :Scene(windowRef) {


}


bool Scene1::OnCreate() {

	camera = nullptr;
	OnResize(windowPtr->GetWidth(), windowPtr->GetHeight());


	/// Load Assets: as needed 
	if (addModel("skull.obj") == false) {
		return false;
	}

	skyBox = new SkyBox("posx.jpg", "negx.jpg", "posy.jpg", "negy.jpg", "posz.jpg", "negz.jpg");
	if (skyBox->OnCreate() == false) {
		return false;
	}	

	/// Create a shader with attributes
	SceneEnvironment::getInstance()->setLight(Vec3(0.0f, 3.0f, 7.0f));

	return true;
}


bool GAME::Scene1::addModel(const char* filename)
{
	models.push_back(new Model(Vec3(0.0f, 0.0f, 0.0f), Vec3(0.0f, 0.0f, 0.0f)));
	models[models.size() - 1]->OnCreate();


	if (models[models.size() - 1]->LoadMesh(filename) == false) {
		return false;
	}
	return true;
}

void Scene1::OnResize(int w_, int h_) {
	windowPtr->SetWindowSize(w_, h_);
	glViewport(0, 0, windowPtr->GetWidth(), windowPtr->GetHeight());
	if (camera) delete camera;
	camera = new Camera(w_, h_, Vec3(0.0f, 0.0f, 10.0f));
	Camera::currentCamera = camera;
	Trackball::getInstance()->setWindowDimensions(windowPtr->GetWidth(), windowPtr->GetHeight());
}

void Scene1::Update(const float deltaTime) {
	for (Model* model : models) {
		model->Update(deltaTime);
	}

}

void Scene1::Render() const {
	glEnable(GL_DEPTH_TEST);
	glEnable(GL_CULL_FACE);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	Matrix4 identiymatrix;
	identiymatrix.loadIdentity();
	skyBox->Render(camera->getProjectionMatrix(), Trackball::getInstance()->getMatrix4());
	

	/// Draw your scene here
	for (Model* model : models) {
		model->Render();

	}
	

	SDL_GL_SwapWindow(windowPtr->getSDLWindow());

}

void Scene1::HandleEvents(const SDL_Event& SDLEvent) {
	if (SDLEvent.type == SDL_EventType::SDL_MOUSEBUTTONDOWN) {
		Trackball::getInstance()->onLeftMouseDown(SDLEvent.button.x, SDLEvent.button.y);
	}
	else if (SDLEvent.type == SDL_EventType::SDL_MOUSEBUTTONUP) {
		Trackball::getInstance()->onLeftMouseUp(SDLEvent.button.x, SDLEvent.button.y);
	}
	else if (SDLEvent.type == SDL_EventType::SDL_MOUSEMOTION &&
		SDL_GetMouseState(NULL, NULL) & SDL_BUTTON(SDL_BUTTON_LEFT)) {
		Trackball::getInstance()->onMouseMove(SDLEvent.button.x, SDLEvent.button.y);
	}

}

Scene1::~Scene1() {
	OnDestroy();
}

void Scene1::OnDestroy() {
	/// Cleanup Assets
	if (camera) delete camera;
	if (skyBox) delete skyBox;
	for (Model* model : models) {
		if (model) delete model;
	}
}