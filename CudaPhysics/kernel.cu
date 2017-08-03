
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include <curand.h>
#include <curand_kernel.h>
#include <stdlib.h>  
#include <stdio.h>

#include <time.h>
#include <assert.h>
#include <math.h>       /* pow */

#define SIZE 1024
#define FLOOR 1000
#define WALL 1000

struct Chromosome {
public:
	short *dna;
};

struct Vector2 {	
public:
	double x;
	double y;
	double length;
	__device__ double Length() {
		return pow(pow(x, 2) + pow(y, 2), 0.5);
	}
};


struct Node {
public: 
	Vector2 position;
	Vector2 velocity;
	int weight;

	
};

struct Muscle {
public:
	int strength;
	Node* nodeA;
	Node* nodeB;

};

struct Animal {
public:
	Node* nodes;
	Muscle* muscles;

};


__device__ int Rand(int min, int max, curandState state) {
	

	int delta = max - min;

	return curand_uniform(&state)*delta+min;
}

__global__ void VectorAdd(Vector2 *a, Vector2 *b, Vector2 *c, int n)
{
    int i = threadIdx.x;
	if (i < n) {
		c[i].x = a[i].length * b[i].length;
	}
}

__global__ void ApplyWalls(Node *a)
{
	int i = threadIdx.x;
	if (a[i].position.x <= 0) {
		a[i].position.x = 1;
		a[i].velocity.x = 0;
	}
	if (a[i].position.x > WALL) {
		a[i].position.x = WALL;
		a[i].velocity.x = 0;
	}
	if (a[i].position.y <= 0) {
		a[i].position.y = 1;
		a[i].velocity.y = 0;
	}
	if (a[i].position.y > FLOOR) {
		a[i].position.y = FLOOR;
		if (a[i].velocity.y > 0) {
			a[i].velocity.y = 0;
		}
	}
}

__global__ void ApplyGravity(Node *a)
{
	int i = threadIdx.x;
	a[i].velocity.y += (double)5 / (double)a[i].weight;
}

__global__ void ApplyMomentum(Node *a) 
{
	int i = threadIdx.x;
	a[i].position.x += a[i].velocity.x / 100;
	a[i].position.y += a[i].velocity.y / 100;
}

__global__ void NodeInit(Node *a)
{
	int i = threadIdx.x;
	a[i].weight = 20;
}

__global__ void AnimalInit(Animal *a) {
	int i = threadIdx.x;
}

__global__ void ChromosomeInit(Chromosome *c, short* o, int length) {
	int i = threadIdx.x;
	curandState state;
	curand_init(clock(), i, 0, &state);
	c[i].dna = new short[length];
	for (int j = 0; j < length; j++) {
		c[i].dna[j] = Rand(1, 200, state);
		o[j] = c[i].dna[j];
	}
}

int main()
{
	short *output = (short *)malloc(SIZE * sizeof(short));
	Node *d_a, *d_b;
	Chromosome *d_c;
	short * d_o;

	cudaSetDevice(0);

	//a = (int *)malloc(SIZE * sizeof(int));
	//b = (int *)malloc(SIZE * sizeof(int));
	//output = (int *)malloc(SIZE * sizeof(int));

	cudaMalloc(&d_a, SIZE * sizeof(Node));
	cudaMalloc(&d_c, SIZE * sizeof(Chromosome));
	cudaMalloc(&d_o, SIZE * sizeof(short));

	//cudaMemcpy(d_a, a, SIZE * sizeof(int), cudaMemcpyHostToDevice);
	//cudaMemcpy(d_b, b, SIZE * sizeof(int), cudaMemcpyHostToDevice);
	//cudaMemcpy(d_c, c, SIZE * sizeof(int), cudaMemcpyHostToDevice);

	NodeInit <<< 1, SIZE >>> (d_a);

	for (int click = 0; click < 100; click++) {
		NodeInit << < 1, SIZE >> > (d_a);
		//ApplyGravity << < 1, SIZE >> > (d_a);
		//ApplyMomentum << < 1, SIZE >> > (d_a);
		//ApplyWalls << < 1, SIZE >> > (d_a);

		cudaError_t cudaStatus = cudaMemcpy(output, d_o, SIZE * sizeof(short), cudaMemcpyDeviceToHost);
		if (cudaStatus != cudaSuccess) {
			fprintf(stderr, "cudaMemcpy failed!");
		}
		else {
			//for (int i = 0; i < 1; i++) {
				int t = 80;
				printf("%d: %d\n", click, output[t]);
			//}
		}
	}



	//free(a);
	//free(b);
	free(output);

	cudaFree(d_a);
	//cudaFree(d_b);
	//cudaFree(d_c);

	getchar();

}