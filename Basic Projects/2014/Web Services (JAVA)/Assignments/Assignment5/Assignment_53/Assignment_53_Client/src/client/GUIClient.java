package client;

import handlers.IWebService;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Container;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Graphics;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.net.URL;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Enumeration;
import java.util.Hashtable;
import java.util.StringTokenizer;
import java.util.Vector;

import javax.swing.BorderFactory;
import javax.swing.DefaultComboBoxModel;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.ListSelectionModel;
import javax.swing.border.Border;
import javax.swing.border.CompoundBorder;
import javax.swing.border.EmptyBorder;
import javax.swing.border.LineBorder;
import javax.swing.table.TableColumnModel;
import javax.swing.table.TableModel;

import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;
import org.apache.xmlrpc.client.util.ClientFactory;

public class GUIClient extends JFrame {

	XMLRPCClient xmlrpcClient = null;
	
	Container contentPane;
	
	//JPanel textButtonPanel; 	// do wyswietlenia statystyk
	
	JPanel mainPanel;
	
	JPanel downloadPanel1;
	JPanel downloadPanel2;
	JPanel downloadPanel3;
	JPanel downloadPanel;
	
	JPanel uploadPanel;
	JPanel uploadPanel1;
	JPanel uploadPanel2;
	JPanel uploadPanel3;
	
	JLabel downloadLabel;
	JLabel uploadLabel;
	
	JTextArea downloadTextArea;
	JScrollPane scroll_downloadTextArea;
	
	JTextArea contentTextArea;
	JScrollPane scroll_contentTextArea;
	
	JTextArea uploadTextArea;
	JScrollPane scroll_uploadTextArea;
	
	JTextArea infoTextArea;
	JScrollPane scroll_infoTextArea;
	
	JFileChooser uploadChooser;
	JComboBox downloadComboBox;
	
	JButton downloadButton;
	JButton addFileButton;
	JButton addFileButton2;	// to Upload
	JButton uploadButton;

	//JFileChooser fileChooser;
	

	public GUIClient() {
		super("Assignment 5-3");
		
		// instance of XML RPC Client
		xmlrpcClient = new XMLRPCClient();
		
		contentPane = this.getContentPane();
		
		mainPanel = new JPanel(new GridLayout(2, 1));
		
		downloadPanel = new JPanel();
		downloadPanel.setBorder(BorderFactory.createLineBorder(Color.black));
		
		downloadPanel1 = new JPanel();
		downloadPanel2 = new JPanel();
		downloadPanel3 = new JPanel();

		downloadLabel = new JLabel("Download Files", JLabel.LEFT);
		//label.setBackground(Color.BLACK);
		downloadTextArea = new JTextArea();
		scroll_downloadTextArea = new JScrollPane(downloadTextArea);
		scroll_downloadTextArea.setPreferredSize(new Dimension(150, 40));
		
		contentTextArea = new JTextArea();
		scroll_contentTextArea = new JScrollPane(contentTextArea);
		scroll_contentTextArea.setPreferredSize(new Dimension(350, 100));
		
		downloadComboBox = new JComboBox();
		String a []= xmlrpcClient.getFileList().split("\n");
		downloadComboBox.setModel(new DefaultComboBoxModel(a));
		
		downloadButton = new JButton("DOWNLOAD");
		downloadButton.addMouseListener(new MouseListener(){

			@Override
			public void mouseClicked(MouseEvent arg0) {
				
				Vector<String> v = new Vector<String>();
				
				StringTokenizer tokenizer = new StringTokenizer(downloadTextArea.getText(), "\n");
				while (tokenizer.hasMoreElements()) {
					v.add(tokenizer.nextElement().toString());
				}
				Hashtable<String, byte[]> ret = xmlrpcClient.myService.downloadFile(v);

				Enumeration<String> enu = ret.keys();
				String key;
				byte [] value;
				while (enu.hasMoreElements()) {
					
					key = enu.nextElement();
					value = ret.get(key);
				
					String decoded = new String(value); 
					
					contentTextArea.setText(contentTextArea.getText() + "Content of file : " + key + "\n" +
							"----------\n" +
							decoded + "\n" +
							"----------\n" +
							"----------\n\n");
				}
			}

			@Override
			public void mouseEntered(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseExited(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mousePressed(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseReleased(MouseEvent arg0) {
				// TODO Auto-generated method stub
			}
		});
		
		addFileButton = new JButton("Add File");
		addFileButton.addMouseListener(new MouseListener(){

			@Override
			public void mouseClicked(MouseEvent e) {
				
				if (downloadComboBox.getSelectedIndex() != 0)
					downloadTextArea.setText(downloadTextArea.getText() + 
						downloadComboBox.getSelectedItem().toString() + "\n");
			}

			@Override
			public void mouseEntered(MouseEvent e) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseExited(MouseEvent e) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mousePressed(MouseEvent e) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseReleased(MouseEvent e) {
				// TODO Auto-generated method stub
				
			}});
		
		
		/////////
		// UPLOAD
		uploadPanel = new JPanel();
		uploadPanel.setBorder(BorderFactory.createLineBorder(Color.black));
		uploadPanel.setBackground(Color.lightGray);
		
		uploadPanel1 = new JPanel();
		uploadPanel1.setBackground(Color.lightGray);
		uploadPanel2 = new JPanel();
		uploadPanel2.setBackground(Color.lightGray);
		uploadPanel3 = new JPanel();
		uploadPanel3.setBackground(Color.lightGray);
		
		uploadLabel = new JLabel("Upload Files", JLabel.LEFT);
		
		uploadTextArea = new JTextArea();
		scroll_uploadTextArea = new JScrollPane(uploadTextArea);
		scroll_uploadTextArea.setPreferredSize(new Dimension(180, 55));
		
		infoTextArea = new JTextArea();
		scroll_infoTextArea = new JScrollPane(infoTextArea);
		scroll_infoTextArea.setPreferredSize(new Dimension(350, 100));
		
		uploadButton = new JButton("UPLOAD");
		uploadButton.addMouseListener(new MouseListener(){

			@Override
			public void mouseClicked(MouseEvent arg0) {
				
				Hashtable<String, byte[]> fileContent = xmlrpcClient.ReadFromFile(uploadTextArea.getText());
				
				String ret = xmlrpcClient.myService.uploadFile(fileContent);
				
				DateFormat dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
				Date date = new Date();
				
				infoTextArea.setText("Operation UPLOAD info on day : " + dateFormat.format(date) + "\n" +
						"----------\n" +
						ret +
						"----------\n");
				
				String a []= xmlrpcClient.getFileList().split("\n");
				downloadComboBox.setModel(new DefaultComboBoxModel(a));
			}

			@Override
			public void mouseEntered(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseExited(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mousePressed(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseReleased(MouseEvent arg0) {
				// TODO Auto-generated method stub
			}
		});
		
		addFileButton2 = new JButton("Add File");
		addFileButton2.addMouseListener(new MouseListener(){

			@Override
			public void mouseClicked(MouseEvent arg0) {
				
				JFileChooser fileChooser = new JFileChooser();
		        int returnValue = fileChooser.showOpenDialog(null);
		        
		        if (returnValue == JFileChooser.APPROVE_OPTION) {
		        	File selectedFile = fileChooser.getSelectedFile();
		        	
		        	uploadTextArea.setText(uploadTextArea.getText() + 
						selectedFile.getAbsolutePath().toString() + "\n");
		        	
		          	//System.out.println(selectedFile.getPath());
		        }
			}

			@Override
			public void mouseEntered(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseExited(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mousePressed(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseReleased(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

		});
		
		// DOWNLOAD
		downloadPanel1.add(downloadLabel);
		downloadPanel1.add(scroll_downloadTextArea);
		downloadPanel1.add(downloadButton);
		downloadPanel2.add(downloadComboBox);
		downloadPanel2.add(addFileButton);
		downloadPanel3.add(scroll_contentTextArea);
		
		downloadPanel.add(downloadPanel1);
		downloadPanel.add(downloadPanel2);
		downloadPanel.add(downloadPanel3);
		
		// UPLOAD
		uploadPanel1.add(uploadLabel);
		uploadPanel1.add(scroll_uploadTextArea);
		uploadPanel1.add(uploadButton);
		uploadPanel2.add(addFileButton2);
		uploadPanel3.add(scroll_infoTextArea);
		
		uploadPanel.add(uploadPanel1);
		uploadPanel.add(uploadPanel2);
		uploadPanel.add(uploadPanel3);
		
		mainPanel.add(downloadPanel);
		mainPanel.add(uploadPanel);

		contentPane.add(mainPanel);

		setBackground(Color.cyan);

		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		setSize(400, 500);

		setLocationRelativeTo(null);
		setVisible(true);
	}

	public static void main(String[] args) {
		
		new GUIClient();
	}
}