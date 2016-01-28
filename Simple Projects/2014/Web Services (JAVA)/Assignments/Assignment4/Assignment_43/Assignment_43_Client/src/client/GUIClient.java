package client;

import handlers.IWebService;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Container;
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

	XMLRPCClient xmlrpcClient = null;
	
	Container contentPane;
	JTable table = null;
	JLabel label;
	JButton addButton;
	JComboBox comboBox;
	JTextField txtf;
	JPanel imagePanel;
	JPanel textButtonPanel;


	public GUIClient() {
		super("Download/ Upload");
		
		// instance of XML RPC Client
		xmlrpcClient = new XMLRPCClient();
		
		
		contentPane = this.getContentPane();

		label = new JLabel();
		label.setBackground(Color.BLACK);
		imagePanel = new JPanel ();
		imagePanel.setLayout(new GridLayout(1, 1));
		imagePanel.add(label);
		
		textButtonPanel = new JPanel();
		textButtonPanel.setLayout(new GridLayout(1, 2));

		//COMBO BOX
		comboBox = new JComboBox();
		//String a[]={"", "heinola.jpg", "barca.jpg"};
		String a []= xmlrpcClient.getImageList().split("\n");
		comboBox.setModel(new DefaultComboBoxModel(a));
		
		addButton = new JButton("Get image");

		addButton.addActionListener(this);

		textButtonPanel.add(comboBox);
		textButtonPanel.add(addButton);

		contentPane.add(imagePanel, BorderLayout.CENTER);
		contentPane.add(textButtonPanel, BorderLayout.SOUTH);

		setBackground(Color.cyan);

		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		setSize(500, 400);

		setLocationRelativeTo(null);
		setVisible(true);
	}

	public static void main(String[] args) {
		
		new GUIClient();
	}

	//@SuppressWarnings("deprecation")
	@Override
	public void actionPerformed(ActionEvent arg0) {

		BufferedImage img = null;
		
		try
		{
			img = xmlrpcClient.getImage(comboBox.getSelectedItem().toString());
		}
		catch(NullPointerException e)
		{ }
		
		if (img != null)
		{
			label.setIcon(new ImageIcon(img));
			label.setText("");
		}
		else
		{
			label.setIcon(null);
			label.setText("null");
		}
	}
}