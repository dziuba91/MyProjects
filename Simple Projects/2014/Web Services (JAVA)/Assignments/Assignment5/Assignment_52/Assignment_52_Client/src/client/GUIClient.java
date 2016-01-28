package client;

import handlers.Dictionary;

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
import java.util.Enumeration;
import java.util.Hashtable;
import java.util.StringTokenizer;
import java.util.Vector;

import javax.swing.DefaultComboBoxModel;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComboBox;
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

public class GUIClient extends JFrame implements ActionListener {

	Client client = null;
	//XMLRPCClient xmlrpcClient = null;
	//IWebService myService;
	
	int testVariable = 0;
	
	Container contentPane;
	JTable table = null;
	JScrollPane spane;
	JLabel label;
	//JTextArea textBox;
	JButton addButton;
	JButton dictionaryEN;
	JButton dictionaryFI;
	
	JTextField txtf;
	JPanel textButtonPanel;
	TableModel tableModel = null;
	TableColumnModel tableColumnModel = null;
	
	JPanel loginPanel;
	JLabel l_login;
	JLabel l_password;
	JTextField txt_login;
	JTextField txt_password;
	
	JLabel emptyLabel;
	JLabel infoLabel;

	Object removableComponent = null;
	JScrollPane scrollPane = null;

	boolean tableAdded = false;

	String login;
	String password;
	
	// Translate Panel
	JLabel labelStatus;
	JComboBox comboBoxStatus;
	JLabel labelWord;
	JLabel emptyLabel2;
	JLabel emptyLabel3;
	JTextArea txt_Word;
	JScrollPane scroll_Word;
	JButton translateButton;
	JTextArea txt_Translated;
	JScrollPane scroll_Translated;
	JPanel translatePanel;
	JPanel translateOptionPanel;
	JPanel translateMainPanel;
	JPanel translateButtonsPanel;
	
	JButton fiDictionaryButton;
	JButton enDictionaryButton;

	public GUIClient() {
		super("Assignment 5-2");
		
		contentPane = this.getContentPane();

		createLoginPanel();
		createTranslatePanel();
		
		
		contentPane.add(loginPanel, BorderLayout.CENTER);  // add login panel
		contentPane.add(textButtonPanel, BorderLayout.SOUTH);
		

		setBackground(Color.cyan);

		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		setSize(500, 200);
		// Here we make the frame appear in the center of the window.
		setLocationRelativeTo(null);
		setVisible(true);
	}

	public static void main(String[] args) {

		new GUIClient();
	}


	//@SuppressWarnings("deprecation")
	@Override
	public void actionPerformed(ActionEvent arg0) {

		if (tableAdded) {

			System.out.println("User: " + login);
			System.out.println("Log out successfully!");
			System.out.println(" --------------------- ");
			
			contentPane.remove(translatePanel);
			
			contentPane.add(loginPanel);
			
			addButton.setText("LOG IN");
			tableAdded = false;

			repaint();
		}
		else {
			
			login = txt_login.getText();
			password = txt_password.getText();
			client = new Client(login, password);
			
			if(client.getLogInStatus())
			{
				System.out.println("Logged successfully as:");
				System.out.println(" -> login: " + txt_login.getText());
				System.out.println(" -> password: " + txt_password.getText());
				System.out.println(" --------------------- ");
				
				
			
				contentPane.remove(loginPanel); // login panel
			
				//scrollPane = new JScrollPane(table);
				contentPane.add(translatePanel, BorderLayout.CENTER);

				addButton.setText("LOG OUT");

				tableAdded = true;
			
				repaint();
			}
			else
			{
				infoLabel.setText("Wrong login or password!");
			}
		}
	}
	
	private void createLoginPanel() {

		// LOGIN PANEL
		loginPanel = new JPanel();
		loginPanel.setLayout(new GridLayout(3, 2));
		l_login = new JLabel("Login : ", JLabel.CENTER);
		txt_login = new JTextField();
		l_password = new JLabel("Password : ", JLabel.CENTER);
		txt_password = new JTextField();
		
		emptyLabel = new JLabel();
		infoLabel = new JLabel(" ", JLabel.CENTER);
		infoLabel.setForeground(Color.red);
		
		loginPanel.add(l_login);
		loginPanel.add(txt_login);
		loginPanel.add(l_password);
		loginPanel.add(txt_password);
		loginPanel.add(emptyLabel);
		loginPanel.add(infoLabel);
		
		
		scrollPane = new JScrollPane();
		textButtonPanel = new JPanel();
		textButtonPanel.setLayout(new GridLayout(1, 2));

		addButton = new JButton("LOG IN");
		addButton.addActionListener(this);

		//textButtonPanel.add(label);
		textButtonPanel.add(addButton);
	}
	
	private void createTranslatePanel() {

		// TRANSLATE PANEL
		translatePanel = new JPanel(new GridLayout(3, 1));
		
		translateMainPanel = new JPanel();
		translateOptionPanel = new JPanel();
		translateButtonsPanel = new JPanel();
		
		//translatePanel.setLayout(
				//new GridLayout(5, 2));
		
		labelStatus = new JLabel("Option :    ", JLabel.RIGHT);

		comboBoxStatus = new JComboBox();
		//String a[]={"", "heinola.jpg", "barca.jpg"};
		String a []= {"EN --> FI", "FI --> EN"};
		comboBoxStatus.setModel(new DefaultComboBoxModel(a));
		
		labelWord = new JLabel("Word translated :    ", JLabel.RIGHT);
		emptyLabel2 = new JLabel("", JLabel.CENTER);
		emptyLabel3 = new JLabel("", JLabel.CENTER);
		
		// TXT1
		txt_Word = new JTextArea();
		//txt_Word.setPreferredSize(new Dimension(100, 20));
		//txt_Word.setSize(new Dimension(100, 20));
		scroll_Word = new JScrollPane(txt_Word);
		scroll_Word.setPreferredSize(new Dimension(150, 40));
		
		// TXT2
		txt_Translated = new JTextArea();
		//txt_Translated.setPreferredSize(new Dimension(100, 20));
		scroll_Translated = new JScrollPane(txt_Translated);
		scroll_Translated.setPreferredSize(new Dimension(150, 40));
		
		translateButton = new JButton("TRANSLATE");
		translateButton.addMouseListener(new MouseListener(){

			@Override
			public void mouseClicked(MouseEvent arg0) {
				// TODO Auto-generated method stub
				Vector<String> v = new Vector<String>();
				
				StringTokenizer tokenizer = new StringTokenizer(txt_Word.getText(), "\n");
				while (tokenizer.hasMoreElements()) {
					v.add(tokenizer.nextElement().toString());
				}
				
				Hashtable<String, String> out = null;
				
				if (comboBoxStatus.getSelectedIndex() == 0)
				{
					out = client.translateToFi(v);
				}
				else
				{
					out = client.translateToEng(v);
				}
				
				txt_Translated.setText("");
				
				//if(out != null || out.size()==0)
				//{
					Enumeration<String> enu = out.keys();
					String key, value;
					int i = 0;
					while (enu.hasMoreElements()) {

						key = enu.nextElement();
						value = out.get(key);
					
						if (value.equals("")) txt_Translated.setText(txt_Translated.getText() + key + " -> " + "(not found)");
						else txt_Translated.setText(txt_Translated.getText() + key + " -> " + value);
						
						i++;
						if(i < out.size()) txt_Translated.setText(txt_Translated.getText() + "\n");
					}
				//}
				//else 
				//{
				//	txt_Translated.setText("Problem with execute operation!");
				//}
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
				
			}});
		
		JButton fiDictionaryButton;
		fiDictionaryButton = new JButton("Show EN-FI DICTIONARY");
		fiDictionaryButton.setForeground(Color.BLACK);
		fiDictionaryButton.setBackground(Color.green);
		Border line = new LineBorder(Color.BLACK);
		Border margin = new EmptyBorder(5, 15, 5, 15);
		Border compound = new CompoundBorder(line, margin);
		fiDictionaryButton.setBorder(compound);
		
		fiDictionaryButton.addMouseListener(new MouseListener() {

			@Override
			public void mouseClicked(MouseEvent arg0) {
				// TODO Auto-generated method stub
				new GUIDictionaryForm(true, client);
				
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
		
		enDictionaryButton = new JButton("Show FI-EN DICTIONARY");
		enDictionaryButton.setForeground(Color.BLACK);
		enDictionaryButton.setBackground(Color.YELLOW);
		enDictionaryButton.setBorder(compound);
		
		enDictionaryButton.addMouseListener(new MouseListener(){

			@Override
			public void mouseClicked(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
				new GUIDictionaryForm(false, client);
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
				
			}});
		
		translateOptionPanel.add(labelStatus);
		translateOptionPanel.add(comboBoxStatus);
		//translatePanel.add(labelWord);
		translateMainPanel.add(scroll_Word);
		translateMainPanel.add(translateButton);
	//	translateMainPanel.add(labelWord, JPanel.RIGHT_ALIGNMENT);
		translateMainPanel.add(scroll_Translated);
		
		//translatePanel.add(emptyLabel2);
		//translatePanel.add(emptyLabel3);
		translateButtonsPanel.add(fiDictionaryButton);
		translateButtonsPanel.add(enDictionaryButton);
		
		translatePanel.add(translateOptionPanel);
		translatePanel.add(translateMainPanel, FlowLayout.CENTER);
		translatePanel.add(translateButtonsPanel);
	}
}