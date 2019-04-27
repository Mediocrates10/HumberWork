#ifndef CUBE_H
#define CUBE_H
#include <vector>
#include "MMath.h"
#include "Mesh.h"
#include "Entity.h"

namespace GAME {

	using namespace MATH;

	class Cube : public Entity {

	protected:

		class Shader *shader;
		unsigned int textureID[1];
		Matrix4 modelMatrix;
		std::vector<Mesh*> meshes;

	public:


		Cube(const Vec3 _pos, const Vec3 _orientation);
		Cube(const Cube&) = delete;
		Cube(Cube&&) = delete;
		Cube& operator = (const Cube&) = delete;
		Cube& operator = (Cube&&) = delete;
		Cube();
		virtual ~Cube();

		void setPos(const Vec3& pos_) override;
		void setOrientation(const Vec3& orienration_) override;

		virtual bool OnCreate();
		virtual void OnDestroy();
		virtual void Render() const;
		virtual void Update(const float deltaTime);

		virtual bool LoadMesh(const char* filename);

	protected:

		void updateModelMatrix();
	};
} /// end of namespace

#endif