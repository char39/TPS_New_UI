using System.IO; // ���� ������� ���� ���ӽ����̽�
using System.Runtime.Serialization.Formatters.Binary;
// ���̳ʸ� ������ ���� ���ӽ����̽� 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
public class DataManager : MonoBehaviour
{
    [SerializeField] string dataPath; //������
    public void Initialize() //���� ��θ� �ʱ�ȭ �ϱ����� �Լ�
    {
        dataPath = Application.persistentDataPath + "/gameD.dat";
        // ���� ���� ��ο� ���ϸ� ���� 
    }
    public void Save(GameData gameData)
    {   
        // ���̳ʸ� ���� ������ ���� BinaryFormatter ����
        BinaryFormatter bf = new BinaryFormatter();
        // ������ ������ ���� ���� ���� 
        FileStream file = File.Create(dataPath);
        //���Ͽ� ������ Ŭ������ ������ �Ҵ�
        GameData data = new GameData();
        data.killCount = gameData.killCount;
        data.hp = gameData.hp;
        data.speed = gameData.speed;
        data.damage = gameData.damage;
        data.equipItem = gameData.equipItem;
         // ����ȭ ������ ��ģ��.
        bf.Serialize(file, data);
        file.Close(); //���� ��Ʈ���� �ݴ´�.
        // �ȴݰ� ���� ������ ���� �Ѵ�. �޸𸮰� ����� ���� ���� ��ä��
        // ������ ���� �ؾ� �ϹǷ� �ݵ�� �ݾƾ� �Ѵ�.
    }
    public GameData Load()
    {        //���� ���� ��ȿ���˻� 
        if(File.Exists(dataPath))
        {
            //������ ���� �ϴ� ��� ������ �ҷ����� 
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);   
            GameData data = (GameData)bf.Deserialize(file);
                                 //������ȭ 
             file.Close();
            return data;
        }
        else
        {
            //������ ���� ��� �⺻���� ��ȯ
            GameData data = new GameData();
            return data;
        }


    }
}
