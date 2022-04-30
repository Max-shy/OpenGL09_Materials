#version 330 core
out vec4 FragColor;

in vec3 FragPos;//����ռ�����
in vec3 Normal;

uniform float mixValue;


uniform vec3 viewPos;//���λ��
uniform vec3 lightColor;
uniform vec3 objectColor;
uniform vec3 lightPos;//��Դλ��

//���ʽṹ��
struct Material{
	vec3 ambient;//��������
	vec3 diffuse;//������ϵ��
	vec3 specular;//���淴��ǿ��
	float shininess;//����ȣ���32
};

//���սṹ��
struct Light{
	vec3 position;//��Դλ��

	vec3 ambient;//������ǿ��
	vec3 diffuse;//������ϵ��
	vec3 specular;//�����ǿ��
};

uniform Material material;//���ʶ���
uniform Light light;//��Դ����

void main()
{
	//������
	vec3 ambient = material.ambient* light.ambient;//��������

	//��һ������
	vec3 norm = normalize(Normal);//��������׼��
	vec3 lightDir = normalize(light.position - FragPos);//���շ���
	//������
	float diff = max(dot(norm,lightDir),0.0);//�����䷽��
	vec3 diffuse = (diff*material.diffuse)*light.diffuse;

	//���淴��
	vec3 viewDir = normalize(viewPos - FragPos);//���߷���
	vec3 reflectDir = reflect(-lightDir, norm);//�������������߷���ȡ��
	//���㾵�����ǿ��
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);//ȡmaterial.shininess���ݵķ����
	vec3 specular = (material.specular* spec) * light.specular;
	

	//��ɫ���(������+�������+���淴��)
	vec3 result = ambient+diffuse+specular;
	FragColor = vec4(result,1.0);
}