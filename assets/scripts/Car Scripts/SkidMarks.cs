using UnityEngine;
using System.Collections;
using System;

[Serializable] //www.youtube.com/marcosschultzunity e www.schultzgames.com
public class RastrVeiculo { //autoria predominante de MARCOS SCHULTZ
	[Range(0.1f,6.0f)] public float larguraDoRastro = 0.3f;
	[Range(1.0f,10.0f)] public float sensibilidade = 2.0f;
	[Range(0.1f,1.0f)]public float OpacidadePadrao = 1.0f;
	[Range(0.001f,0.1f)]public float DistanciaDoChao = 0.02f;
	public Color CorPadrao = new Color(0.15f,0.15f,0.15f,0);
	public Shader shaderDerrapagens;
	[Space(10)]
	[Range(0.0f,1.0f)]public float intensidadeNormalMap = 0.7f;
	[Range(0.0f,1.0f)]public float smoothness = 0.0f;
	[Range(0.0f,1.0f)]public float metallic = 0.0f;
	[Space(10)]
	public TerrenosDiversosRastros[] OutrosTerrenos;
}
[Serializable]
public class TerrenosDiversosRastros {
	public bool rastroContinuo = true;
	public string tagChao = "chao";
	public Color corDoRastro = new Color(0.5f,0.2f,0.0f,0);
	[Range(0.1f,1.0f)]public float OpacidadeDoRastro = 0.8f;
}
[Serializable]
public class RodasVeicl {
//	public _ClasseRoda RodaFrenteDir;
//	public _ClasseRoda RodaFrenteEsq;
	public _ClasseRoda RodaTrazDir;
	public _ClasseRoda RodaTrazEsq;
//	public _ClasseRoda[] RodasExtraVeiculo;
}
[Serializable]
public class _ClasseRoda {
	public WheelCollider ColliderDaRoda;
	[Range(-2.0f,2.0f)] public float deslocamentoRastro = 0.0f;
	[HideInInspector] public Mesh rendSKDmarks;
	[HideInInspector] public bool gerandoRastro;
}
public class SkidMarks : MonoBehaviour {

	public RastrVeiculo RastrosDoVeiculo;
	public RodasVeicl RodasDoVeiculo;
	private float KMh;
	private bool ativarRastros;
	private Rigidbody corpoRigido;
	private Vector3[] last = new Vector3[54];

	void Start(){
		corpoRigido = GetComponent<Rigidbody> ();
		if (RastrosDoVeiculo.shaderDerrapagens != null) {
			SetarValoresRastros ();
			ativarRastros = true;
		} else {
			ativarRastros = false;
		}
	}

	void Update(){
		KMh = corpoRigido.velocity.magnitude * 3.6f;
	}

	void LateUpdate(){
		if (ativarRastros == true) {
			ChecarChaoParaRastros ();
		}
	}

	void ChecarChaoParaRastros(){
		/*if (RodasDoVeiculo.RodaFrenteDir.ColliderDaRoda != null) {
			if (RodasDoVeiculo.RodaFrenteDir.ColliderDaRoda.isGrounded) {
				RodasDoVeiculo.RodaFrenteDir.gerandoRastro = GerarRastroDasRodas (RodasDoVeiculo.RodaFrenteDir.ColliderDaRoda, RodasDoVeiculo.RodaFrenteDir.rendSKDmarks, RodasDoVeiculo.RodaFrenteDir.gerandoRastro, RodasDoVeiculo.RodaFrenteDir.deslocamentoRastro,0);
			} else {
				RodasDoVeiculo.RodaFrenteDir.gerandoRastro = false;
			}
		}
		//
		if (RodasDoVeiculo.RodaFrenteEsq.ColliderDaRoda != null) {
			if (RodasDoVeiculo.RodaFrenteEsq.ColliderDaRoda.isGrounded) {
				RodasDoVeiculo.RodaFrenteEsq.gerandoRastro = GerarRastroDasRodas (RodasDoVeiculo.RodaFrenteEsq.ColliderDaRoda, RodasDoVeiculo.RodaFrenteEsq.rendSKDmarks, RodasDoVeiculo.RodaFrenteEsq.gerandoRastro, RodasDoVeiculo.RodaFrenteEsq.deslocamentoRastro,1);
			} else {
				RodasDoVeiculo.RodaFrenteEsq.gerandoRastro = false;
			}
		}*/
		//
		if (RodasDoVeiculo.RodaTrazDir.ColliderDaRoda != null) {
			if (RodasDoVeiculo.RodaTrazDir.ColliderDaRoda.isGrounded) {
				RodasDoVeiculo.RodaTrazDir.gerandoRastro = GerarRastroDasRodas (RodasDoVeiculo.RodaTrazDir.ColliderDaRoda, RodasDoVeiculo.RodaTrazDir.rendSKDmarks, RodasDoVeiculo.RodaTrazDir.gerandoRastro, RodasDoVeiculo.RodaTrazDir.deslocamentoRastro,2);
			} else {
				RodasDoVeiculo.RodaTrazDir.gerandoRastro = false;
			}
		}
		//
		if (RodasDoVeiculo.RodaTrazEsq.ColliderDaRoda != null) {
			if (RodasDoVeiculo.RodaTrazEsq.ColliderDaRoda.isGrounded) {
				RodasDoVeiculo.RodaTrazEsq.gerandoRastro = GerarRastroDasRodas (RodasDoVeiculo.RodaTrazEsq.ColliderDaRoda, RodasDoVeiculo.RodaTrazEsq.rendSKDmarks, RodasDoVeiculo.RodaTrazEsq.gerandoRastro, RodasDoVeiculo.RodaTrazEsq.deslocamentoRastro,3);
			} else {
				RodasDoVeiculo.RodaTrazEsq.gerandoRastro = false;
			}
		}
		//
	/*	for (int x = 0; x < RodasDoVeiculo.RodasExtraVeiculo.Length; x++) {
			if (RodasDoVeiculo.RodasExtraVeiculo [x].ColliderDaRoda != null) {
				if (RodasDoVeiculo.RodasExtraVeiculo [x].ColliderDaRoda.isGrounded) {
					RodasDoVeiculo.RodasExtraVeiculo[x].gerandoRastro = GerarRastroDasRodas (RodasDoVeiculo.RodasExtraVeiculo [x].ColliderDaRoda, RodasDoVeiculo.RodasExtraVeiculo[x].rendSKDmarks , RodasDoVeiculo.RodasExtraVeiculo[x].gerandoRastro, RodasDoVeiculo.RodasExtraVeiculo [x].deslocamentoRastro,(x+4));
				} else {
					RodasDoVeiculo.RodasExtraVeiculo[x].gerandoRastro = false;
				}
			}
		}*/
	}
	private bool GerarRastroDasRodas(WheelCollider colisor, Mesh meshDaRoda, bool variavelBooleana, float deslocamentoLateral, int indiceLastMark) {
		WheelHit hit;
		colisor.GetGroundHit (out hit);
		var vertices = meshDaRoda.vertices;
		var normals = meshDaRoda.normals;
		var tris = meshDaRoda.triangles;
		var colors = meshDaRoda.colors;
		var uv = meshDaRoda.uv;
		var alpha = Mathf.Abs(hit.sidewaysSlip);
		var skid = hit.sidewaysDir * RastrosDoVeiculo.larguraDoRastro / 2f * Vector3.Dot(colisor.attachedRigidbody.velocity.normalized, hit.forwardDir);
		skid -= hit.forwardDir * RastrosDoVeiculo.larguraDoRastro * 0.1f * Vector3.Dot(colisor.attachedRigidbody.velocity.normalized, hit.sidewaysDir);
		if(KMh > (75.0f/RastrosDoVeiculo.sensibilidade) && Mathf.Abs(colisor.rpm) < (3.0f / RastrosDoVeiculo.sensibilidade)){ 
			if (colisor.isGrounded) {
				alpha = 10;
			}
		}
		float maximaDerrapagem = 1.2f/RastrosDoVeiculo.sensibilidade;
		if (KMh < 20.0f * (Mathf.Clamp (RastrosDoVeiculo.sensibilidade, 1, 3))) {
			if (Mathf.Abs(hit.forwardSlip) > maximaDerrapagem) {
				if (colisor.isGrounded) {
					alpha = 10;
				}
			}
		}
		for (int x = 0; x < RastrosDoVeiculo.OutrosTerrenos.Length; x++) {
			string tagg = RastrosDoVeiculo.OutrosTerrenos [x].tagChao;
			if (hit.collider.gameObject.tag == tagg) {
				if (RastrosDoVeiculo.OutrosTerrenos [x].rastroContinuo == true) {
					alpha = 10;
				}
				break;
			}
		}
		if (alpha < (1 / RastrosDoVeiculo.sensibilidade)) {
			return false;
		}
		float distance = (last[indiceLastMark] - hit.point - skid).sqrMagnitude;
		float alphaAplic = Mathf.Clamp (alpha, 0.0f, 1.0f);
		if(variavelBooleana) {
			if (distance < 0.04f) {
				return true;
			}
			Array.Resize(ref tris, tris.Length + 6);
		}
		Array.Resize(ref vertices, vertices.Length + 2);
		Array.Resize(ref normals, normals.Length + 2);
		Array.Resize(ref colors, colors.Length + 2);
		Array.Resize(ref uv, uv.Length + 2);
		var verLenght = vertices.Length;
		vertices[verLenght - 1] = hit.point + hit.normal * RastrosDoVeiculo.DistanciaDoChao - skid + hit.sidewaysDir * deslocamentoLateral;
		vertices[verLenght - 2] = hit.point + hit.normal * RastrosDoVeiculo.DistanciaDoChao + skid + hit.sidewaysDir * deslocamentoLateral;
		normals[verLenght - 1] = normals[verLenght - 2] = hit.normal;
		//
		Color corRastro = RastrosDoVeiculo.CorPadrao;
		corRastro.a = Mathf.Clamp (alphaAplic * RastrosDoVeiculo.OpacidadePadrao, 0.01f, 1.0f);
		for (int x = 0; x < RastrosDoVeiculo.OutrosTerrenos.Length; x++) {
			string tagg = RastrosDoVeiculo.OutrosTerrenos [x].tagChao;
			if (hit.collider.gameObject.tag == tagg) {
				corRastro = RastrosDoVeiculo.OutrosTerrenos [x].corDoRastro;
				corRastro.a = Mathf.Clamp (alphaAplic * RastrosDoVeiculo.OutrosTerrenos [x].OpacidadeDoRastro, 0.01f, 1.0f);
				break;
			}
		}
		colors[verLenght - 1] = colors[verLenght - 2] = corRastro;
		//

		if(variavelBooleana) {
			tris[tris.Length - 1] = verLenght - 2;
			tris[tris.Length - 2] = verLenght - 1;
			tris[tris.Length - 3] = verLenght - 3;
			tris[tris.Length - 4] = verLenght - 3;
			tris[tris.Length - 5] = verLenght - 4;
			tris[tris.Length - 6] = verLenght - 2;
			uv[verLenght - 1] = uv[verLenght - 3] + Vector2.right * distance*0.01f;
			uv[verLenght - 2] = uv[verLenght - 4] + Vector2.right * distance*0.01f;
		}
		else {
			uv[verLenght - 1] = Vector2.zero;
			uv[verLenght - 2] = Vector2.up;
		}
		last[indiceLastMark] = vertices [vertices.Length - 1];
		meshDaRoda.vertices = vertices;
		meshDaRoda.normals = normals;
		meshDaRoda.triangles = tris;
		meshDaRoda.colors = colors;
		meshDaRoda.uv = uv;
		return true;
	}

	Mesh GerarRendRef(Material skdMaterial){
		GameObject rendRef = new GameObject("SKDMark");
		rendRef.AddComponent<MeshFilter>();
		rendRef.AddComponent<MeshRenderer>();
		rendRef.hideFlags = HideFlags.HideAndDontSave;
		rendRef.GetComponent<MeshFilter>().mesh = new Mesh();
		rendRef.GetComponent<MeshRenderer>().material = skdMaterial;
		rendRef.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		rendRef.GetComponent<MeshFilter> ().mesh.MarkDynamic ();
		return rendRef.GetComponent<MeshFilter>().mesh;
	}
	void SetarValoresRastros(){
		Material skidmarkMaterial = new Material( RastrosDoVeiculo.shaderDerrapagens );
		skidmarkMaterial.mainTexture = CriarTexturaRastros ();
		skidmarkMaterial.SetTexture ("_NormalMap", CriarNormalMapRastros ());
		skidmarkMaterial.SetFloat ("_NormFactor", RastrosDoVeiculo.intensidadeNormalMap);
		skidmarkMaterial.SetFloat ("_Glossiness", RastrosDoVeiculo.smoothness);
		skidmarkMaterial.SetFloat ("_Metallic", RastrosDoVeiculo.metallic);
		Color corRastro = RastrosDoVeiculo.CorPadrao;
		corRastro.a = RastrosDoVeiculo.OpacidadePadrao; 
		skidmarkMaterial.color = corRastro;
		//
//		RodasDoVeiculo.RodaFrenteDir.rendSKDmarks = GerarRendRef(skidmarkMaterial);
//		RodasDoVeiculo.RodaFrenteEsq.rendSKDmarks = GerarRendRef(skidmarkMaterial);
		RodasDoVeiculo.RodaTrazDir.rendSKDmarks = GerarRendRef(skidmarkMaterial);
		RodasDoVeiculo.RodaTrazEsq.rendSKDmarks = GerarRendRef(skidmarkMaterial);
		/*for (int x = 0; x < RodasDoVeiculo.RodasExtraVeiculo.Length; x++) {
			RodasDoVeiculo.RodasExtraVeiculo[x].rendSKDmarks = GerarRendRef(skidmarkMaterial);
		}*/
	}
	public Texture CriarTexturaRastros(){
		var texture = new Texture2D(32, 32, TextureFormat.ARGB32, false);
		Color corTransparente1 = new Color (1.0f, 1.0f, 1.0f, 0.15f);
		Color corTransparente2 = new Color (1.0f, 1.0f, 1.0f, 0.6f);
		for (int x = 0; x < 32; x++) {
			for (int y = 0; y < 32; y++) {
				texture.SetPixel(x, y, Color.white);
			}
		}
		for (int y = 0; y < 32; y++) {
			for (int x = 0; x < 32; x++) {
				if (y == 0 || y == 1 || y == 30 || y == 31) {
					texture.SetPixel (x, y, corTransparente1);
				}
				if (y == 6 || y == 7 || y == 15 || y == 16 || y == 24 || y == 25) {
					texture.SetPixel (x, y, corTransparente2);
				}
			}
		}
		texture.Apply();
		return texture;
	}
	public Texture CriarNormalMapRastros(){
		var texture = new Texture2D(32, 32, TextureFormat.ARGB32, false);
		Color corTransparente1 = new Color (0.0f, 0.0f, 0.0f, 0.5f);
		Color corTransparente2 = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		for (int x = 0; x < 32; x++) {
			for (int y = 0; y < 32; y++) {
				texture.SetPixel(x, y, Color.white);
			}
		}
		for (int y = 0; y < 32; y++) {
			for (int x = 0; x < 32; x++) {
				if (y == 0 || y == 1 || y == 30 || y == 31) {
					texture.SetPixel (x, y, corTransparente1);
				}
				if (y == 6 || y == 7 || y == 15 || y == 16 || y == 24 || y == 25) {
					texture.SetPixel (x, y, corTransparente2);
				}
			}
		}
		texture.Apply();
		return texture;
	}
}