# OpenCLDotNet

OpenCL Dot Net is a C# wrapper around OpenCL. The wrapper hopefully simplifies the process of setting up the OpenCL context and running programs on the GPU.

A OpenCL program can come from text file in binary or text format or it can come from a string written in the source code itself. 

Below we have a simple example of a program that writes the contents of one image into another.

var program_text =
@"__kernel void read_write_test(__read_only image2d_t srcImg,
				__write_only image2d_t dstImg,
				sampler_t sampler,
				int width, int height)
			{
				
			        int2 imageCoord = (int2)(get_global_id(0), get_global_id(1));
				if (imageCoord.x < width && imageCoord.y < height)
				{
					float4 outColor = read_imagef(srcImg, sampler,imageCoord);
					write_imagef(dstImg, imageCoord, outColor);
				}
			}
			";
			
The first step is to take the program text and create a proram object.		
			
var program = new CLProgram(program_text);

Next we create a array and fill it with some arbritry values.

var data = new float[SIZE];
for (int i = 0; i < data.Length; i++)
	data[i] = i;	
	
You will see the programs kernel has 5 arguments. Two images, a sampler and two integers.
We will create the two images parameter structs first using a default float image settings.
We will provide the read image with the data array that will be copied into the image on creation.
				
var read_image_param = CLImageParameters2D.FloatImage(WIDTH, HEIGHT, CHANNELS);
read_image_param.Source = data;

var write_image_param = CLImageParameters2D.FloatImage(WIDTH, HEIGHT, CHANNELS);

Next we create rest of the kernel parameters.

var kernel_params = new CLKernelParameter()
{
	//The kernel name must be provided so the program 
	//knows which kernel to apply the arguments to.
	Name = "read_write_test",
	
	//The kernels dimension. In this case its 2D but 1D or 3D sre also options
	Dimension = 2,
	
	//The work group local and global sizes. 
	GlobalSize = new CLPoint3t(WIDTH, HEIGHT, 1),
	LocalSize = new CLPoint3t(16, 16, 1),

	//Next we have the kernels aruments. We create the two images from the parameter structs, 
	//a default index based sampler and the images width and height.
	Args = new()
	{
		new CLKernelArgParameter(0, program.CreateReadImage2D(read_image_param)),
		new CLKernelArgParameter(1, program.CreateWriteImage2D(write_image_param)),
		new CLKernelArgParameter(2, program.CreateSamplerIndex()),
		new CLKernelArgParameter(3, WIDTH),
		new CLKernelArgParameter(4, HEIGHT)
	}
};

We can now run the program.

program.Run(kernel_params);

If a issue occured when the program was ran a error code is provided.

Console.WriteLine(program.Error);

The image that the data was copied into can be fetched by providing the kernel name and its argument index.

var image = program.GetImage("read_write_test", 1);

The images contents can be read back to the CPU and the we print the first 100 values.

var result = new float[SIZE];
image.Read(program.Command, result);

for (int i = 0; i < 100; i++)
	Console.WriteLine(result[i]);
			

